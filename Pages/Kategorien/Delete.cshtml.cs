using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using System.Threading.Tasks;

namespace LagerverwaltungApp.Pages.Kategorien
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kategorie Kategorie { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Kategorie? kategorie = await _context.Kategorien.FirstOrDefaultAsync(m => m.Id == id);
            if (kategorie == null)
                return NotFound();

            Kategorie = kategorie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Kategorie? kategorie = await _context.Kategorien.FindAsync(id);
            if (kategorie != null)
            {
                _context.Kategorien.Remove(kategorie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
