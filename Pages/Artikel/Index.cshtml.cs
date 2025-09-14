using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using ArtikelModel = LagerverwaltungApp.Models.Produkt;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LagerverwaltungApp.Pages.Artikel
{
    [Authorize(Roles = "Admin,Manager")]

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ArtikelModel> ArtikelListe { get; set; } = new();

        public async Task OnGetAsync()
        {
            ArtikelListe = await _context.Artikel.ToListAsync();
        }
    }
}
