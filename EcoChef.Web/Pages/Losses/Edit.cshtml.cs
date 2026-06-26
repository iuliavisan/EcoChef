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

namespace EcoChef.Web.Pages.Losses
{
    public class EditModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public EditModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pierdere Pierdere { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pierdere =  await _context.Pierderi.FirstOrDefaultAsync(m => m.Id == id);
            if (pierdere == null)
            {
                return NotFound();
            }
            Pierdere = pierdere;
           ViewData["IngredientId"] = new SelectList(_context.Ingrediente, "Id", "Nume");
            return Page();
        }

      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pierdere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PierdereExists(Pierdere.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PierdereExists(int id)
        {
            return _context.Pierderi.Any(e => e.Id == id);
        }
    }
}
