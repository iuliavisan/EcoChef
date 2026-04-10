using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public IndexModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;
        public Dictionary<string, List<Ingredient>> IngredientePeCategorie { get; set; } = new();

        public async Task OnGetAsync()
        {
            var ingrediente = await _context.Ingrediente
                .OrderBy(i => i.Categorie)
                .ToListAsync();

            IngredientePeCategorie = ingrediente
                .GroupBy(i => i.Categorie)
                .ToDictionary(g => g.Key, g=> g.ToList());
        }
    }
}
