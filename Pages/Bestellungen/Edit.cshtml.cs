using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Bestellungen
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bestellung Bestellung { get; set; } = default!;

        public SelectList ArtikelListe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var bestellung = await _context.Bestellungen
                .Include(b => b.Artikel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bestellung == null) return NotFound();

            Bestellung = bestellung;
            ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ArtikelListe = new SelectList(_context.Artikel, "Id", "Name");
                return Page();
            }

            // Bestellung anhängen und aktualisieren
            _context.Attach(Bestellung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bestellungen.Any(e => e.Id == Bestellung.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
