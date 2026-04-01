using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Losses
{
    public class DeleteModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DeleteModel(EcoChef.Web.Data.ApplicationDbContext context)
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

            var pierdere = await _context.Pierderi.FirstOrDefaultAsync(m => m.Id == id);

            if (pierdere == null)
            {
                return NotFound();
            }
            else
            {
                Pierdere = pierdere;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pierdere = await _context.Pierderi.FindAsync(id);
            if (pierdere != null)
            {
                Pierdere = pierdere;
                _context.Pierderi.Remove(Pierdere);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
