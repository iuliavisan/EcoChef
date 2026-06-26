using EcoChef.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcoChef.Web.Models;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Migrations;

namespace EcoChef.Web.Pages.Cooking
{
  
    public class IndexModel : PageModel 
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reteta> Retete { get; set; } = new List<Reteta>(); 
        public List<IstoricGatire> Istoric { get; set; }
        public DateTime DataSelectata { get; set; } = DateTime.Today;
        public void OnGet(DateTime? data)
        {
            DataSelectata = data ?? DateTime.Today;
        
            Retete = _context.Retete.ToList(); 

            Istoric = _context.IstoricGatire
                .Where(g => g.DataGatirii.Date == DataSelectata.Date)
                .Include(g => g.Reteta)
                .OrderByDescending(g => g.DataGatirii)
                .ToList();
        }

        public IActionResult OnPost(int RetetaId, int NrPortii) 
        {
            Retete = _context.Retete.ToList();
            
            var ingrediente = _context.IngredientRetete 
                .Where(rand => rand.RetetaId == RetetaId)
                .Include(rand => rand.Ingredient)
                .ToList();

            foreach(var ingredient_reteta in ingrediente)
            {
                var cantitateTotal = ingredient_reteta.CantitateNecesara * NrPortii;

                if(ingredient_reteta.Ingredient.StocCurent < cantitateTotal)
                {
                    ModelState.AddModelError("",
                        $"Stoc insuficient pentru {ingredient_reteta.Ingredient.Nume}. " +
                        $"Ai {ingredient_reteta.Ingredient.StocCurent} în stoc, îți trebuie {cantitateTotal}.");
                }
            }

            if (!ModelState.IsValid)
            {
                Istoric = _context.IstoricGatire
                    .Where(g => g.DataGatirii.Date == DateTime.Today)
                    .Include(g => g.Reteta)
                    .ToList();
                return Page();
            }

            foreach(var ingredient_reteta in ingrediente)
            {
                var cantitateTotal = ingredient_reteta.CantitateNecesara * NrPortii;
                ingredient_reteta.Ingredient.StocCurent -= cantitateTotal;
            }

            _context.SaveChanges();
            var setari = _context.Setari.FirstOrDefault();
            var marja = setari?.MarjaProfit ?? 30m;

            var costTotal = ingrediente.Sum(ir => ir.CantitateNecesara * NrPortii * ir.Ingredient.PretAchizitie);
            var pretVanzareTotal = costTotal + (costTotal * marja / 100);

            var gatire = new IstoricGatire
            {
                RetetaId = RetetaId,
                NrPortii = NrPortii,
                DataGatirii = DateTime.Now,
                CostTotal = costTotal,
                PretVanzareTotal = pretVanzareTotal
            };

            _context.IstoricGatire.Add(gatire);
            _context.SaveChanges();

            Istoric = _context.IstoricGatire
                .Where(g => g.DataGatirii.Date == DateTime.Today)
                .Include(g => g.Reteta)
                .ToList();

            return Page(); 
        }

        public async Task<IActionResult> OnPostStergeAsync(int id)
        {
            var gatire = await _context.IstoricGatire
                .Include(g => g.Reteta)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gatire == null) return NotFound();

            //pt a readauga stocul:
            var ingrediente = await _context.IngredientRetete
                .Where(ir => ir.RetetaId == gatire.RetetaId)
                .Include(ir => ir.Ingredient)
                .ToListAsync();

            foreach (var ir in ingrediente)
            {
                ir.Ingredient.StocCurent += ir.CantitateNecesara * gatire.NrPortii;

            }

            _context.IstoricGatire.Remove(gatire);
            await _context.SaveChangesAsync();

            return RedirectToPage(); 
        }
    }
}
