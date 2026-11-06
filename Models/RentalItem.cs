
namespace RentalCRM.Models
{
    public class RentalItem
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public Rental? Rental { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal DailyRate { get; set; }

        public decimal DepositPerUnit { get; set; }
    }
}
