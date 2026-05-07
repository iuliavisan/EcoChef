using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DetailsModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Reteta Reteta { get; set; } = default!;
        public List<IngredientReteta> Ingrediente { get; set; } = new();
        public decimal MarjaProfit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reteta = await _context.Retete
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Reteta == null)
            {
                return NotFound();
            }

            Ingrediente = await _context.IngredientRetete
                .Where(ir => ir.RetetaId == id)
                .Include(ir => ir.Ingredient)
                .ToListAsync();

            var setari = await _context.Setari.FirstOrDefaultAsync();
            MarjaProfit = setari?.MarjaProfit ?? 30;

            return Page();
        }
    }
}
