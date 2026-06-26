using System; //biblioteca principala Microsoft
using System.ComponentModel.DataAnnotations; //reguli



namespace EcoChef.Web.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele ingredientului este obligatoriu!")]
        public required string Nume { get; set; }

        [Required(ErrorMessage = "Unitatea de măsură este obligatorie!")]
        [Display(Name = "Unitate măsură")]

        public required string UnitateMasura { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Prețul per unitate de masură trebuie să fie mai mare decât 0!")]
        [Display(Name = "Preț achiziție per unitate de măsură")]

        public decimal PretAchizitie { get; set; }

        [Required(ErrorMessage = "Menționarea stocului curent este obligatoriu!")]
        [Display(Name = "Stoc curent")]

        public decimal StocCurent { get; set; }

        [Required(ErrorMessage = "Data expirării este obligatorie!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dată expirare")]

        public DateTime DataExpirare { get; set; }

        [Display(Name = "Categorie")]
        public string Categorie { get; set; } = "Altele";
    }
}
