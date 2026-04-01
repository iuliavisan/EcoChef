using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoChef.Web.Models
{
    public class IngredientReteta
    {
        public int Id { get; set; }

        //legatura cu reteta
        public int RetetaId { get; set; } //nr retetei
        public Reteta? Reteta { get; set; } //reteta propriu-zisa, cu nume,timp,etc

        //?-poate fi null

        //legatura cu ingredientul
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CantitateNecesara { get; set; }
    }
}
