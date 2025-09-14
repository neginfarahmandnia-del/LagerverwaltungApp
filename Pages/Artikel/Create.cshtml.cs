using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Artikel
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produkt Artikel { get; set; } = default!;

        public List<SelectListItem> KategorienListe { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            KategorienListe = await _context.Kategorien
                .Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Name
                }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                KategorienListe = await _context.Kategorien
                    .Select(k => new SelectListItem
                    {
                        Value = k.Id.ToString(),
                        Text = k.Name
                    }).ToListAsync();

                return Page();
            }

            _context.Artikel.Add(Artikel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
