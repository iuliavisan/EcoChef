using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.IngredientsRecipes
{
    public class CreateModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public CreateModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IngredientId"] = new SelectList(_context.Ingrediente, "Id", "Nume");
        ViewData["RetetaId"] = new SelectList(_context.Retete, "Id", "Nume");
            return Page();
        }

        [BindProperty]
        public IngredientReteta IngredientReteta { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.IngredientRetete.Add(IngredientReteta);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
