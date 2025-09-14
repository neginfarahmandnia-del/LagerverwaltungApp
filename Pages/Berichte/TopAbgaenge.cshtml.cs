using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class TopAbgaengeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TopAbgaengeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Jahr { get; set; } = DateTime.Now.Year;

        [BindProperty(SupportsGet = true)]
        public int Monat { get; set; } = DateTime.Now.Month;

        public List<(string ArtikelName, int Menge)> TopAbgaenge { get; set; } = new();
        public List<string> Labels { get; set; } = new();
        public List<int> Werte { get; set; } = new();

        public void OnGet()
        {
            var start = new DateTime(Jahr, Monat, 1);
            var end = start.AddMonths(1);

            TopAbgaenge = _context.Lagerabgaenge
                .Include(x => x.Artikel)
                .Where(x => x.Datum >= start && x.Datum < end && x.Artikel != null)
                .GroupBy(x => x.Artikel!.Name)
                .Select(g => new
                {
                    ArtikelName = g.Key,
                    Menge = g.Sum(x => x.Menge)
                })
                .OrderByDescending(x => x.Menge)
                .Take(10)
                .AsEnumerable()
                .Select(x => (x.ArtikelName, x.Menge))
                .ToList();

            Labels = TopAbgaenge.Select(x => x.ArtikelName).ToList();
            Werte = TopAbgaenge.Select(x => x.Menge).ToList();
        }
    }
}
