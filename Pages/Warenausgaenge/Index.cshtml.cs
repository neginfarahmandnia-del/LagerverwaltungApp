using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Warenausgaenge
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<LagerabgangEintrag> Abgaenge { get; set; } = [];

        public async Task OnGetAsync()
        {
            Abgaenge = await _context.Lagerabgaenge
                .Include(a => a.Artikel)
                .Include(a => a.Benutzer)
                .OrderByDescending(a => a.Datum)
                .ToListAsync();
        }
    }
}
