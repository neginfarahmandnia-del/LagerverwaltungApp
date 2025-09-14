using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.LagerAbgang
{
    public class ErstellenModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ErstellenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LagerabgangEintrag Abgang { get; set; } = new();

        public List<SelectListItem> ArtikelListe { get; set; } = new();
        public List<SelectListItem> Gründe { get; set; } = new()
{
    new SelectListItem("Defekt", "Defekt"),
    new SelectListItem("Ablaufdatum", "Ablaufdatum"),
    new SelectListItem("Verloren", "Verloren"),
    new SelectListItem("Interne Verwendung", "Interne Verwendung"),
    new SelectListItem("Inventuranpassung", "Inventuranpassung")
};

        public async Task<IActionResult> OnGetAsync()
        {
            ArtikelListe = await _context.Artikel
                .Where(a => a.Bestand > 0)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var artikel = await _context.Artikel.FindAsync(Abgang.ArtikelId);
            if (artikel == null || artikel.Bestand < Abgang.Menge)
            {
                ModelState.AddModelError(string.Empty, "Nicht genügend Bestand oder Artikel nicht gefunden.");
                await OnGetAsync();
                return Page();
            }

            artikel.Bestand -= Abgang.Menge;
            Abgang.Datum = DateTime.Now;
            _context.Lagerabgaenge.Add(Abgang);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
