using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LagerverwaltungApp.Models
{
    public class Warenausgang
    {
        public int Id { get; set; }

        [Required]
        public int ArtikelId { get; set; }
        [BindNever]
        [ForeignKey("ArtikelId")]
        public Produkt Artikel { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Die Menge muss größer als 0 sein.")]
        public int Menge { get; set; }

        public DateTime Datum { get; set; } = DateTime.Now;

        public int? BenutzerId { get; set; }
        [BindNever]
        [ForeignKey("BenutzerId")]
        public Benutzer? Benutzer { get; set; }
        public string? Grund { get; set; }
        public string? Kommentar { get; set; }

    }
}
