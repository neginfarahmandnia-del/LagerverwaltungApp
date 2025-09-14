using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LagerverwaltungApp.Models
{
    public class Produkt
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Beschreibung { get; set; }

        public int Bestand { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preis { get; set; }
        [Required]
        public int Mindestbestand { get; set; } = 0;
        // Fremdschlüssel zur Kategorie
        public int KategorieId { get; set; }

        // Navigationseigenschaft zur Kategorie
        public Kategorie? Kategorie { get; set; }
    }
}
