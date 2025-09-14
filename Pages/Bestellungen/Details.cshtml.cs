using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LagerverwaltungApp.Models;
using LagerverwaltungApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Bestellungen
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Bestellung? Bestellung { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bestellungen == null)
            {
                return NotFound();
            }

            Bestellung = await _context.Bestellungen
                .Include(b => b.Artikel)
                .Include(b => b.Benutzer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Bestellung == null)
            {
                return NotFound();
            }

            return Page();

        }
    }
}
