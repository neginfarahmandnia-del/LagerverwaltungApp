namespace LagerverwaltungApp.Models
{
    public class Kategorie
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Produkt> ArtikelListe { get; set; } = new();

    }
}
