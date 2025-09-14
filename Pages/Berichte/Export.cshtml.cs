using LagerverwaltungApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LagerverwaltungApp.Pages.Berichte
{
    public class ExportModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ExportModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public string? DownloadLink { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var daten = await _context.Lagerabgaenge
                .Include(x => x.Artikel)
                .Include(x => x.Benutzer)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Datum;Artikel;Grund;Vorher;Nachher;Benutzer");

            foreach (var d in daten)
            {
                sb.AppendLine($"{d.Datum:yyyy-MM-dd};{d.Artikel?.Name};{d.Grund};{d.VorherMenge};{d.NachherMenge};{d.Benutzer?.Benutzername}");
            }

            var fileName = $"lager_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var filePath = Path.Combine(_env.WebRootPath, "exports", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await System.IO.File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8);

            DownloadLink = "/exports/" + fileName;
            return Page();
        }
    }
}
