using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcoChef.Web.Data;
using EcoChef.Web.Models;
using System.Text.Json;

namespace EcoChef.Web.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context; 
        }

        public string ReteteJson { get; set; } = "[]";
        public string PierderiJson {  get; set; }= "[]";
        public decimal TotalProfit { get; set; }
        public decimal TotalPierderi { get; set; }
        public DateTime LunaSelectata { get; set; }

        public async Task OnGetAsync(int? an, int? luna)
        {
            var azi = DateTime.Now;
            LunaSelectata = new DateTime(an ?? azi.Year, luna ?? azi.Month, 1);
            var sfarsitLuna = LunaSelectata.AddMonths(1);

            //cele mai gatite retete
            var reteteGatite = await _context.IstoricGatire
                .Where(g => g.DataGatirii >= LunaSelectata && g.DataGatirii < sfarsitLuna)
                .Include(g => g.Reteta)
                .GroupBy(g => g.Reteta.Nume)
                .Select(g => new
                {
                    Nume = g.Key,
                    TotalPortii = g.Sum(x => x.NrPortii)
                })
                .OrderByDescending(g => g.TotalPortii)
                .ToListAsync();

            //cele mai mari pierderi

            var pierderi = await _context.Pierderi
                .Where(p => p.DataPierdere >= LunaSelectata && p.DataPierdere < sfarsitLuna)
                .Include(p => p.Ingredient)
                .GroupBy(p => p.Ingredient.Nume)
                .Select(g => new
                {
                    Nume = g.Key,
                    TotalPierdere = g.Sum(x => x.PretPierdere)
                })
                .OrderByDescending(g => g.TotalPierdere)
                .ToListAsync();

            //totaluri
            var istoricLuna = await _context.IstoricGatire
                .Where(g => g.DataGatirii >= LunaSelectata && g.DataGatirii < sfarsitLuna)
                .ToListAsync();

            TotalProfit = istoricLuna.Sum(g => g.PretVanzareTotal - g.CostTotal);
            TotalPierderi = pierderi.Sum(p => p.TotalPierdere);

            //serializare pt js
            ReteteJson = JsonSerializer.Serialize(reteteGatite);
            PierderiJson = JsonSerializer.Serialize(pierderi);
        }
    }
}
