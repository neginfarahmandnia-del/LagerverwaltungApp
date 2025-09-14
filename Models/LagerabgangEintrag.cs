using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LagerverwaltungApp.Models
{
    public class LagerabgangEintrag
    {
        public int Id { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public string Grund { get; set; } = string.Empty;

        // Artikel
        public int ArtikelId { get; set; }
        public Produkt Artikel { get; set; } = null!;

        public int VorherMenge { get; set; }
        public int NachherMenge { get; set; }

        // 🔽 Beziehung zu ApplicationUser (Benutzer)
        public string? BenutzerId { get; set; }

        [ForeignKey("BenutzerId")]
        public ApplicationUser? Benutzer { get; set; }
        [Required]
        public int Menge { get; set; }

        public string? Kommentar { get; set; }
    }
}
