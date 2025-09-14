using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.LagerAbgang
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<LagerabgangEintrag> Abgaenge { get; set; } = new();

        public async Task OnGetAsync()
        {
            Abgaenge = await _context.Lagerabgaenge
                .Include(a => a.Artikel) // Falls Beziehung existiert
                .OrderByDescending(a => a.Datum)
                .ToListAsync();
        }
    }
}
