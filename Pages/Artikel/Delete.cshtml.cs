using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;

namespace LagerverwaltungApp.Pages.Artikel
{
    public class DeleteModel : PageModel
    {
        private readonly LagerverwaltungApp.Data.ApplicationDbContext _context;

        public DeleteModel(LagerverwaltungApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LagerverwaltungApp.Models.Produkt Artikel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.Artikel.FirstOrDefaultAsync(m => m.Id == id);

            if (artikel == null)
            {
                return NotFound();
            }
            else
            {
                Artikel = artikel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.Artikel.FindAsync(id);
            if (artikel != null)
            {
                Artikel = artikel;
                _context.Artikel.Remove(Artikel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
