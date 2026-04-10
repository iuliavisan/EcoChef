using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.IngredientsRecipes
{
    public class IndexModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public IndexModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IngredientReteta> IngredientReteta { get;set; } = default!;
        public Dictionary<string, List<IngredientReteta>> IngredientePeRetete { get; set; } = new();
        public async Task OnGetAsync()
        {
            IngredientReteta = await _context.IngredientRetete
                .Include(i => i.Ingredient)
                .Include(i => i.Reteta)
                .OrderBy(i => i.Reteta.Nume)
                .ToListAsync();

            IngredientePeRetete = IngredientReteta
                .GroupBy(i => i.Reteta.Nume)
                .ToDictionary(g => g.Key, g=>g.ToList());
        }
    }
}
