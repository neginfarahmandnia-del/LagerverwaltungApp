using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class NiedrigerBestandModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NiedrigerBestandModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Produkt> ProdukteMitNiedrigemBestand { get; set; } = new();

        public void OnGet()
        {
            ProdukteMitNiedrigemBestand = _context.Artikel 
                .Where(p => p.Bestand <= p.Mindestbestand)
                .OrderBy(p => p.Bestand)
                .ToList();
        }
    }
}
