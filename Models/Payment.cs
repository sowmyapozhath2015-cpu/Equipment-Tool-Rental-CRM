
namespace RentalCRM.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public Rental? Rental { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string PaymentType { get; set; } = "Rental";

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
