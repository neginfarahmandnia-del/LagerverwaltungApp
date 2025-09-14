using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace LagerverwaltungApp.Pages.Bestellungen
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bestellung Bestellung { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var bestellung = await _context.Bestellungen.FindAsync(id);

            if (bestellung == null)
                return NotFound();

            Bestellung = bestellung;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var bestellung = await _context.Bestellungen.FindAsync(Bestellung.Id);

            if (bestellung != null)
            {
                _context.Bestellungen.Remove(bestellung);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
