using System.ComponentModel.DataAnnotations;
namespace EcoChef.Web.Models
{
    public class Setari
    {
        public int Id { get; set; }
        [Display(Name ="Marjă profit (%)")]
        public decimal MarjaProfit { get; set; }
    }
}
