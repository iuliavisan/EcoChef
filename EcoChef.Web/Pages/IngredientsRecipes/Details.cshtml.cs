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
    public class DetailsModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DetailsModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
