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

namespace EcoChef.Web.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public EditModel(EcoChef.Web.Data.ApplicationDbContext context)
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

            var reteta =  await _context.Retete.FirstOrDefaultAsync(m => m.Id == id);
            if (reteta == null)
            {
                return NotFound();
            }
            Reteta = reteta;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? poza)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var pozaVeche = (await _context.Retete
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.Id == Reteta.Id))?.ImagineReteta;

            if(poza != null && poza.Length > 0)
            {
                var numeFisier = Guid.NewGuid().ToString() + Path.GetExtension(poza.FileName);
                var caleFisier = Path.Combine("wwwroot", "img", numeFisier);

                using(var stream = new FileStream(caleFisier, FileMode.Create))
                {
                    await poza.CopyToAsync(stream);
                }
                Reteta.ImagineReteta = numeFisier;
            }
            else
            {
                Reteta.ImagineReteta = pozaVeche;
            }

                _context.Attach(Reteta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetetaExists(Reteta.Id))
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

        private bool RetetaExists(int id)
        {
            return _context.Retete.Any(e => e.Id == id);
        }
    }
}
