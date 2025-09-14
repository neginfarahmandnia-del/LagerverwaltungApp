using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class AbgaengeNachGrundModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AbgaengeNachGrundModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<StatistikEintrag> Statistik { get; set; } = new();
        public List<string> Labels { get; set; } = new();
        public List<int> Werte { get; set; } = new();

        public class StatistikEintrag
        {
            public string ArtikelName { get; set; } = "";
            public string Grund { get; set; } = "";
            public int Menge { get; set; }
        }

        public async Task OnGetAsync()
        {
            var daten = await _context.Lagerabgaenge
                .Include(x => x.Artikel)
                .Where(x => x.Artikel != null)
                .GroupBy(x => new { x.Artikel!.Name, x.Grund })
                .Select(g => new StatistikEintrag
                {
                    ArtikelName = g.Key.Name,
                    Grund = g.Key.Grund ?? "Unbekannt",
                    Menge = g.Sum(x => x.Menge)
                })
                .ToListAsync();

            Statistik = daten;
            Labels = daten.Select(d => $"{d.ArtikelName} ({d.Grund})").ToList();
            Werte = daten.Select(d => d.Menge).ToList();
        }
    }
}
