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
        public List<Reteta> ReteteRecomandate { get; set; }=new List<Reteta> ();

        public async Task OnGetAsync()
        {
            var limita = DateTime.Now.AddDays(3);
            IngredienteExpira = await _context.Ingrediente
                .Where(i => i.DataExpirare <= limita)
                .ToListAsync();

            //id urile ingredientelor care expira
            var idExpira = IngredienteExpira.Select(i => i.Id).ToList();

            //gaseste retete care contin cel putin un ingredient care expira
            ReteteRecomandate = await _context.Retete
                .Where(r=> r.IngredientReteta
                    .Any(ir => idExpira.Contains(ir.IngredientId)))
                .Include(r => r.IngredientReteta)
                    .ThenInclude(ir => ir.Ingredient)
                .ToListAsync();

        }
    }
}
