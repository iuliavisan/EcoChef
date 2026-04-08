using System.ComponentModel.DataAnnotations;


namespace EcoChef.Web.Models
{
    public class IstoricGatire
    {
        public int Id { get; set; }
        public int RetetaId { get; set; }
        public Reteta? Reteta { get; set; }

        [Required]
        public int NrPortii { get; set; }
        public DateTime DataGatirii { get; set; } = DateTime.Now;
        public decimal CostTotal { get; set; }
        public decimal PretVanzareTotal { get; set; }
    }
}
