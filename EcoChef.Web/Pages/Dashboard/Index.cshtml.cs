using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;


namespace EcoChef.Web.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> IngredienteExpira { get; set; } = new List<Ingredient>();
        public List<Reteta> ReteteRecomandate { get; set; } = new List<Reteta>();

        public async Task OnGetAsync()
        {
            var azi = DateTime.Now;
            var limita = DateTime.Now.AddDays(3);
            IngredienteExpira = await _context.Ingrediente
                .Where(i => i.DataExpirare >= azi && i.DataExpirare <= limita)
                .ToListAsync();

            var idExpira = IngredienteExpira.Select(i => i.Id).ToList();

            ReteteRecomandate = await _context.Retete
                .Where(r => r.IngredientReteta
                    .Any(ir => idExpira.Contains(ir.IngredientId)))
                .Include(r => r.IngredientReteta)
                    .ThenInclude(ir => ir.Ingredient)
                .ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            {
                var azi = DateTime.Now;

                var ingredienteExpirate = await _context.Ingrediente
                    .Where(i => i.DataExpirare < azi && i.StocCurent > 0)
                    .ToListAsync();

                foreach (var ingredient in ingredienteExpirate)
                {
                    var pierdere = new Pierdere
                    {
                        IngredientId = ingredient.Id,
                        CantitatePierdere = ingredient.StocCurent,
                        MotivPierdere = "Expirat automat",
                        DataPierdere = azi,
                        PretPierdere = ingredient.StocCurent * ingredient.PretAchizitie
                    };
                    _context.Pierderi.Add(pierdere);

                    ingredient.StocCurent = 0;
                }
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
        }
    }
}
        
    

