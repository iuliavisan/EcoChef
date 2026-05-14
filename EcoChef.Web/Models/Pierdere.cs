using System.ComponentModel.DataAnnotations;

namespace EcoChef.Web.Models
{
    public class Pierdere
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingredientul este obligatoriu!")]
        [Display(Name = "Ingredient")]

        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; } //ia TOT din Ingredient

        [Range(0.1, double.MaxValue, ErrorMessage = "Cantitatea pierderii este obligatorie!")]
        [Display(Name = "Cantitate")]
        public decimal CantitatePierdere { get; set; }

        [Required(ErrorMessage ="Motivul pierderii este necesar!")]
        [Display(Name = "Motivul pierderii")]
        public string MotivPierdere { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data pierderii")]
        public DateTime DataPierdere{ get; set; }

        [Display(Name = "Preț pierdere")]
        public decimal PretPierdere { get; set; }
    }
}
