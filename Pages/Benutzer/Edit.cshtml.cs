// Pages/Benutzer/Edit.cshtml.cs
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LagerverwaltungApp.Pages.Benutzer
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            public string Id { get; set; } = string.Empty;
            [Required]
            public string UserName { get; set; } = string.Empty;
            [Required]
            public string Vorname { get; set; } = string.Empty;
            [Required]
            public string Nachname { get; set; } = string.Empty;
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;
            public string Rolle { get; set; } = "Mitarbeiter";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            Input = new InputModel
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Vorname = user.Vorname ?? string.Empty,
                Nachname = user.Nachname ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Rolle = user.Rolle ?? string.Empty
            };


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            user.UserName = Input.UserName;
            user.Vorname = Input.Vorname;
            user.Nachname = Input.Nachname;
            user.Email = Input.Email;
            user.Rolle = Input.Rolle;

            await _userManager.UpdateAsync(user);
            return RedirectToPage("Index");
        }
    }
}
