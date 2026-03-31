using System; //biblioteca principala Microsoft
using System.ComponentModel.DataAnnotations; //pentru reguli

//get->aplicatia are voie sa citeasca informatia
//set->aplicatia are voie sa modifice si sa salveze info

namespace EcoChef.Web.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele ingredientului este obligatoriu!")]
        public required string Nume { get; set; }

        [Required(ErrorMessage = "Unitatea de măsură este obligatorie!")]
        public required string UnitateMasura { get; set; }

        [Required(ErrorMessage = "Prețul per unitate de masură este obligatoriu!")]
        public decimal PretAchizitie { get; set; }

        [Required(ErrorMessage = "Menționarea stocului curent este obligatoriu!")]
        public decimal StocCurent { get; set; }

        [Required(ErrorMessage = "Data expirării este obligatorie!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataExpirare { get; set; }
    }
}
