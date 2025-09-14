using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LagerverwaltungApp.Pages.Warenausgaenge
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int ArtikelId { get; set; }

        [BindProperty]
        public int Menge { get; set; }

        [BindProperty]
        public string? Kommentar { get; set; }

        public List<Produkt> ArtikelListe { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            ArtikelListe = await _context.Artikel.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var artikel = await _context.Artikel.FindAsync(ArtikelId);

            if (artikel == null)
            {
                ModelState.AddModelError("", "Artikel nicht gefunden.");
                return Page();
            }

            if (Menge <= 0 || Menge > artikel.Bestand)
            {
                ModelState.AddModelError("", "Ungültige Menge.");
                return Page();
            }

            // BenutzerId vom eingeloggten Benutzer holen
            var benutzerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var vorher = artikel.Bestand;
            var nachher = vorher - Menge;

            // Bestand ändern
            artikel.Bestand = nachher;

            // Lagerabgang speichern
            var abgang = new LagerabgangEintrag
            {
                ArtikelId = ArtikelId,
                Datum = DateTime.Now,
                VorherMenge = vorher,
                NachherMenge = nachher,
                Menge = Menge,
                Kommentar = Kommentar,
                Grund = "Warenausgaenge",
                BenutzerId = benutzerId
            };

            _context.Lagerabgaenge.Add(abgang);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Warenausgaenge/Index");
        }
    }
}
