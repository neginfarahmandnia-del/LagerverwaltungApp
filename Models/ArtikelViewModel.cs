using Microsoft.AspNetCore.Mvc.Rendering;

namespace LagerverwaltungApp.Models
{
    public class ArtikelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Bestand { get; set; }

        public int KategorieId { get; set; }

        // Für DropDown
        public List<SelectListItem> Kategorien { get; set; } = new();
    }

}
