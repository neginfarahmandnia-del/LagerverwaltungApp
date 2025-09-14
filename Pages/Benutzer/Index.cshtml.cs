// Pages/Benutzer/Index.cshtml.cs
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Pages.Benutzer
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<ApplicationUser> BenutzerListe { get; set; } = new();

        public async Task OnGetAsync()
        {

            BenutzerListe = await _userManager.Users.ToListAsync();
        }
    }
}
