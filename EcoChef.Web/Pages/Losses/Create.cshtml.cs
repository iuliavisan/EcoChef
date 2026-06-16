using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Losses
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
        ViewData["Unitati"] = _context.Ingrediente
            .ToDictionary(i => i.Id, i => i.UnitateMasura);
        return Page();
        }

        [BindProperty] //puteai face si onpost cu argumente dar aici ai mai multe campuri de adus
        public Pierdere Pierdere { get; set; } = default!; //pentru o clasa, valoarea default este null

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)//verifica validarile
            {
                return Page();
            }

            var ingredient = await _context.Ingrediente.FindAsync(Pierdere.IngredientId);
            
            Pierdere.PretPierdere = Pierdere.CantitatePierdere * ingredient.PretAchizitie;

            ingredient.StocCurent -= Pierdere.CantitatePierdere;

            _context.Pierderi.Add(Pierdere);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
