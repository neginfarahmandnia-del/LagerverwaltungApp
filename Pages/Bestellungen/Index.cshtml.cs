using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Models;
using LagerverwaltungApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace LagerverwaltungApp.Pages.Bestellungen
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Bestellung> Bestellungen { get; set; } = new List<Bestellung>();

        [BindProperty(SupportsGet = true)]
        public string? Suchbegriff { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Bestellungen
                .Include(b => b.Artikel)
                .Include(b => b.Benutzer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(Suchbegriff))
            {
                query = query.Where(b => b.Artikel!.Name.Contains(Suchbegriff));
            }

            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    query = query.Where(b => b.BenutzerId == user.Id);
                }
            }

            Bestellungen = await query.OrderByDescending(b => b.Bestelldatum).ToListAsync();
        }

        public async Task<FileResult> OnGetExportExcelAsync()
        {
            var bestellungen = await _context.Bestellungen
                .Include(b => b.Artikel)
                .Include(b => b.Benutzer)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Bestellungen");

            worksheet.Cell(1, 1).Value = "Artikel";
            worksheet.Cell(1, 2).Value = "Menge";
            worksheet.Cell(1, 3).Value = "Bestelldatum";
            worksheet.Cell(1, 4).Value = "Benutzer";

            for (int i = 0; i < bestellungen.Count; i++)
            {
                var b = bestellungen[i];
                worksheet.Cell(i + 2, 1).Value = b.Artikel?.Name ?? "Unbekannt";
                worksheet.Cell(i + 2, 2).Value = b.Menge;
                worksheet.Cell(i + 2, 3).Value = b.Bestelldatum.ToShortDateString();
                worksheet.Cell(i + 2, 4).Value = b.Benutzer?.UserName ?? "Unbekannt";
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bestellungen.xlsx");
        }
    }
}
