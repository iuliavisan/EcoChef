using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;

namespace EcoChef.Web.Pages.IngredientsRecipes
{
    public class IndexModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public IndexModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IngredientReteta> IngredientReteta { get;set; } = default!;

        public async Task OnGetAsync()
        {
            IngredientReteta = await _context.IngredientRetete
                .Include(i => i.Ingredient)
                .Include(i => i.Reteta).ToListAsync();
        }
    }
}
