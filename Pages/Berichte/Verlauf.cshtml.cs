using LagerverwaltungApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class VerlaufModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VerlaufModel(ApplicationDbContext context) => _context = context;

        public List<BestandsverlaufDto> Verlauf { get; set; } = [];

        public async Task OnGetAsync()
        {
            Verlauf = await _context.Lagerabgaenge
                .Include(e => e.Artikel)
                .Include(e => e.Benutzer)
                .OrderByDescending(e => e.Datum)
                .Select(e => new BestandsverlaufDto
                {
                    Datum = e.Datum,
                    ArtikelName = e.Artikel!.Name,
                    Vorher = e.VorherMenge,
                    Aenderung = e.VorherMenge - e.NachherMenge,
                    Nachher = e.NachherMenge,
                    Benutzername = e.Benutzer!.Benutzername
                })
                .ToListAsync();
        }

        public class BestandsverlaufDto
        {
            public DateTime Datum { get; set; }
            public string ArtikelName { get; set; } = string.Empty;
            public int Vorher { get; set; }
            public int Aenderung { get; set; }
            public int Nachher { get; set; }
            public string Benutzername { get; set; } = string.Empty;
        }
    }
}
