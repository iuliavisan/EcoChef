using System.ComponentModel.DataAnnotations;

namespace EcoChef.Web.Models
{
    public class Pierdere
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele produsului este obligatoriu!")]
        [Display(Name = "Numele produsului de scăzut din stoc")]

        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required(ErrorMessage = "Cantitatea pierderii este obligatorie!")]
        [Display(Name = "Cantitate:")]
        public decimal CantitatePierdere { get; set; }

        [Required(ErrorMessage ="Motivul pierderii este necesar!")]
        [Display(Name = "Motivul pierderii:")]
        public string MotivPierdere { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data pierderii")]
        public DateTime DataPierdere{ get; set; }


        public decimal PretPierdere { get; set; }
    }
}
