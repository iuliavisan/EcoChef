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
    public class IndexModel : PageModel
    {
        private readonly EcoChef.Web.Data.ApplicationDbContext _context;

        public IndexModel(EcoChef.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pierdere> Pierdere { get;set; } = default!;
        public Dictionary<DateTime, List<Pierdere>> PierderiPeZile { get; set; } = new();
        public DateTime DataSelectata { get; set; } = DateTime.Today;

        public async Task OnGetAsync(DateTime? data)
        {
            DataSelectata = data ?? DateTime.Today;

            Pierdere = await _context.Pierderi
                .Include(p => p.Ingredient)
                .Where(p=> p.DataPierdere.Date == DataSelectata.Date)
                .OrderByDescending(p=> p.DataPierdere)
                .ToListAsync();

            PierderiPeZile = Pierdere
                .GroupBy(p => p.DataPierdere.Date)
                .ToDictionary(g => g.Key, g=> g.ToList());
        }
    }
}
