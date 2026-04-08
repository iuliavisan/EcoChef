using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Settings
{
    public class DeleteModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DeleteModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Setari Setari { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setari = await _context.Setari.FirstOrDefaultAsync(m => m.Id == id);

            if (setari == null)
            {
                return NotFound();
            }
            else
            {
                Setari = setari;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setari = await _context.Setari.FindAsync(id);
            if (setari != null)
            {
                Setari = setari;
                _context.Setari.Remove(Setari);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
