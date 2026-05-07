using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;
using EcoChef.Web.Migrations;

namespace EcoChef.Web.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public IndexModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reteta> Reteta { get;set; } = default!;

        public decimal MarjaProfit { get; set; }
        public Dictionary<string, List<Reteta>> RetetePerCategorie { get; set; } = new(); 
        public async Task OnGetAsync()
        {
            var setari = await _context.Setari.FirstOrDefaultAsync();
            MarjaProfit = setari?.MarjaProfit ?? 30m;

            Reteta = await _context.Retete
                .Include(reteta => reteta.IngredientReteta)
                    .ThenInclude(ingredient_reteta => ingredient_reteta.Ingredient)
                .ToListAsync();

            RetetePerCategorie = Reteta
                .GroupBy(r=>r.Categorie)
                .ToDictionary(g=>g.Key, g=>g.ToList());
        }
    }
}
