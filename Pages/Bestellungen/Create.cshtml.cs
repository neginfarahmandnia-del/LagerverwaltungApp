using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Bestellungen
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Bestellung Bestellung { get; set; } = default!;

        public SelectList ArtikelListe { get; set; } = default!;


        public void OnGet()
        {
            ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");
                return Page();
            }

            var artikel = await _context.Artikel
                .FirstOrDefaultAsync(a => a.Id == Bestellung.ArtikelId);

            if (artikel == null)
            {
                ModelState.AddModelError("Bestellung.ArtikelId", "❌ Artikel wurde nicht gefunden.");
                ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");
                return Page();
            }
            if (Bestellung.Menge > artikel.Bestand)
            {
                ModelState.AddModelError("Bestellung.Menge", $"❌ Ungültige Menge. Nur {artikel.Bestand} Stück von '{artikel.Name}' auf Lager.");
                ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");
                return Page();
            }
            if (artikel.Bestand - Bestellung.Menge < artikel.Mindestbestand)
            {
                TempData["Warnung"] = $"⚠️ Achtung: Bestand von '{artikel.Name}' fällt unter den Mindestbestand!";
            }
            Bestellung.ArtikelId = artikel.Id;


            var user = await _userManager.GetUserAsync(User);
            Bestellung.BenutzerId = user?.Id;
            Bestellung.Bestelldatum = Bestellung.Bestelldatum == DateTime.MinValue
                ? DateTime.Today
                : Bestellung.Bestelldatum;

            _context.Bestellungen.Add(Bestellung);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

