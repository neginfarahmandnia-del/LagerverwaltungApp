// Pages/Benutzer/Create.cshtml.cs
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LagerverwaltungApp.Pages.Benutzer
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public CreateInputModel Input { get; set; } = new();

        public class CreateInputModel
        {
            [Required]
            public string UserName { get; set; } = string.Empty;
            [Required]
            public string Vorname { get; set; } = string.Empty;
            [Required]
            public string Nachname { get; set; } = string.Empty;
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string Rolle { get; set; } = "Mitarbeiter";
            [Required, DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new ApplicationUser
            {
                UserName = Input.UserName,
                Email = Input.Email,
                Vorname = Input.Vorname,
                Nachname = Input.Nachname,
                Rolle = Input.Rolle
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Input.Rolle);
                return RedirectToPage("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}
