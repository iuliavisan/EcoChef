using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.Recipes
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
            return Page();
        }

        [BindProperty]
        public Reteta Reteta { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(IFormFile? poza)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (poza != null && poza.Length > 0)
            {
                var numeFisier = Guid.NewGuid().ToString() + Path.GetExtension(poza.FileName);
                var caleFisier = Path.Combine("wwwroot", "img", numeFisier);

                using (var stream = new FileStream(caleFisier, FileMode.Create))
                {
                    await poza.CopyToAsync(stream);
                }

                Reteta.ImagineReteta = numeFisier;
            }

            _context.Retete.Add(Reteta);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}