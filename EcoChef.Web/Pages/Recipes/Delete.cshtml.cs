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
    public class DeleteModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public DeleteModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reteta Reteta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reteta = await _context.Retete.FirstOrDefaultAsync(m => m.Id == id);

            if (reteta == null)
            {
                return NotFound();
            }
            else
            {
                Reteta = reteta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reteta = await _context.Retete.FindAsync(id);
            if (reteta != null)
            {
                Reteta = reteta;
                _context.Retete.Remove(Reteta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
