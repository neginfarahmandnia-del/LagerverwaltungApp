namespace LagerverwaltungApp.Models
{
    // Models/Bestellung.cs
    public class Bestellung
    {
        public int Id { get; set; }
        public int ArtikelId { get; set; }
        public Produkt? Artikel { get; set; }
        public int Menge { get; set; }
        public DateTime Bestelldatum { get; set; }
        public string? BenutzerId { get; set; }
        public ApplicationUser? Benutzer { get; set; }
        public List<Bestellposition> Positionen { get; set; } = new();

    }

}
