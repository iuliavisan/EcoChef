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
    public class DeleteModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DeleteModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IngredientReteta IngredientReteta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientreteta = await _context.IngredientRetete.FirstOrDefaultAsync(m => m.Id == id);

            if (ingredientreteta == null)
            {
                return NotFound();
            }
            else
            {
                IngredientReteta = ingredientreteta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredientreteta = await _context.IngredientRetete.FindAsync(id);
            if (ingredientreteta != null)
            {
                IngredientReteta = ingredientreteta;
                _context.IngredientRetete.Remove(IngredientReteta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
