using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using System.Threading.Tasks;

namespace LagerverwaltungApp.Pages.Kategorien
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kategorie Kategorie { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Kategorie? kategorie = await _context.Kategorien.FindAsync(id);
            if (kategorie == null)
                return NotFound();

            Kategorie = kategorie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Kategorie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}