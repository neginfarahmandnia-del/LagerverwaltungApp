namespace LagerverwaltungApp.Models
{
    public class Bestellposition
    {
        public int Id { get; set; }

        public int BestellungId { get; set; }
        public required Bestellung Bestellung { get; set; }


        public int ArtikelId { get; set; }
        public required Produkt Artikel { get; set; }

        public int Menge { get; set; }
    }

}
