using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using System.Threading.Tasks;

namespace LagerverwaltungApp.Pages.Kategorien
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kategorie Kategorie { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Kategorien.Add(Kategorie);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
