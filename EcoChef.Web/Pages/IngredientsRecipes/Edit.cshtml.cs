using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.IngredientsRecipes
{
    public class EditModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public EditModel(EcoChef.Web.Data.ApplicationDbContext context)
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

            var ingredientreteta =  await _context.IngredientRetete.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientreteta == null)
            {
                return NotFound();
            }
            IngredientReteta = ingredientreteta;
           ViewData["IngredientId"] = new SelectList(_context.Ingrediente, "Id", "Nume");
           ViewData["RetetaId"] = new SelectList(_context.Retete, "Id", "Nume");
            ViewData["Unitati"] = _context.Ingrediente
                .ToDictionary(i => i.Id, i => i.UnitateMasura);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(IngredientReteta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientRetetaExists(IngredientReteta.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Recipes/Details", new { id = IngredientReteta.RetetaId }); ;
        }

        private bool IngredientRetetaExists(int id)
        {
            return _context.IngredientRetete.Any(e => e.Id == id);
        }
    }
}
