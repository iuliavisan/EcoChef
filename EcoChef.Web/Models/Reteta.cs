using System.ComponentModel.DataAnnotations;

namespace EcoChef.Web.Models
{
    public class Reteta
    {
    //functioneaza la fel id si fara [key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele rețetei este obligatoriu!")]
        public required string Nume {  get; set; }

        [Display(Name = "Instrucțiuni Preparare")]
        public string? Instructiuni { get; set; }

        [Display(Name = "Timp (minute)")]
        public int TimpPreparare { get; set; }

        public ICollection<IngredientReteta>? IngredientReteta { get; set; }

        public decimal PretVanzare { get; set; }

        public decimal MarjaProfit { get; set; }
    }// <> lista care contine obicecte de tip IngredientReteta
    //? pt ca lista poate fi nulla
}
