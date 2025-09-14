using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;

namespace LagerverwaltungApp.Pages.Artikel
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produkt? Artikel { get; set; }

        public SelectList KategorieListe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Artikel = await _context.Artikel
                .Include(a => a.Kategorie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Artikel == null)
                return NotFound();

            KategorieListe = new SelectList(_context.Kategorien, "Id", "Name", Artikel.KategorieId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                KategorieListe = new SelectList(_context.Kategorien, "Id", "Name", Artikel?.KategorieId);
                return Page();
            }

            if (Artikel == null)
                return NotFound();

            try
            {
                _context.Attach(Artikel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikelExists(Artikel.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ArtikelExists(int id)
        {
            return _context.Artikel.Any(e => e.Id == id);
        }
    }


}
