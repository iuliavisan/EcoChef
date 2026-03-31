namespace EcoChef.Web.Models
{
    public class Pierdere
    {
        public int Id { get; set; }

        public string NumePierdere { get; set; }
        public decimal CantitatePierdere { get; set; }

        public string MotivPierdere { get; set; }

        public DateTime DataPierdere{ get; set; }

        public decimal PretPierdere { get; set; }
    }
}
