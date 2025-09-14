using LagerverwaltungApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class AbgaengeProBenutzerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AbgaengeProBenutzerModel(ApplicationDbContext context) => _context = context;

        public List<BenutzerAbgangStat> BenutzerAbgaenge { get; set; } = [];

        public async Task OnGetAsync()
        {
            BenutzerAbgaenge = await _context.Lagerabgaenge
                .Include(l => l.Benutzer)
.GroupBy(l => l.Benutzer!.UserName)
                .Select(g => new BenutzerAbgangStat
                {
                    Benutzername = g.Key ?? "Unbekannt",
                    Anzahl = g.Count()
                })
                .ToListAsync();
        }

        public class BenutzerAbgangStat
        {
            public string Benutzername { get; set; } = string.Empty;
            public int Anzahl { get; set; }
        }
    }
}
