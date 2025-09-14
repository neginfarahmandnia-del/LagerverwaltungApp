using Microsoft.AspNetCore.Identity;

namespace LagerverwaltungApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string Benutzername => UserName!;
        public string Rolle { get; set; } = "Mitarbeiter";

    }
}
