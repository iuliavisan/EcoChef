using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoChef.Web.Models
{
    public class IngredientReteta
    {
        public int Id { get; set; } 

        public int RetetaId { get; set; } 
        public Reteta? Reteta { get; set; } 
       
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CantitateNecesara { get; set; }
    }
}
