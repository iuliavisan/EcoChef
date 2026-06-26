using System.ComponentModel.DataAnnotations;

namespace EcoChef.Web.Models
{
    public class Reteta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele rețetei este obligatoriu!")]
        public required string Nume {  get; set; }

        [Display(Name = "Instrucțiuni Preparare")]
        public string? Instructiuni { get; set; }

        [Display(Name = "Timp (minute)")]
        public int TimpPreparare { get; set; }

        [Display(Name = "Categorie")]
        public string Categorie { get; set; } = "Altele";
        public string? ImagineReteta { get; set; }

        public ICollection<IngredientReteta>? IngredientReteta { get; set; }

    }
}
