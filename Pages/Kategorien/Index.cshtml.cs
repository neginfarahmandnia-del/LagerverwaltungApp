// Pages/Kategorien/Index.cshtml.cs
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Kategorien
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Kategorie> Kategorien { get; set; } = new();

        public async Task OnGetAsync()
        {
            Kategorien = await _context.Kategorien.ToListAsync();
        }
    }
}