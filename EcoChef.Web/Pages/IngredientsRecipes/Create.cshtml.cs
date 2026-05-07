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

        public IActionResult OnGet(int? retetaId)
        {
            if (retetaId.HasValue)
            {
                IngredientRetetaForm = new IngredientReteta { RetetaId = retetaId.Value };
            }
        ViewData["IngredientId"] = new SelectList(_context.Ingrediente, "Id", "Nume");
        ViewData["RetetaId"] = new SelectList(_context.Retete, "Id", "Nume");
            ViewData["Unitati"] = _context.Ingrediente
                .ToDictionary(i => i.Id, i => i.UnitateMasura);
            return Page();
        }

        [BindProperty]
        public IngredientReteta IngredientRetetaForm { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.IngredientRetete.Add(IngredientRetetaForm);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipes/Details", new {id = IngredientRetetaForm.RetetaId});
        }
    }
}
