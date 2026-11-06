
using System.ComponentModel.DataAnnotations;

namespace RentalCRM.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public DateTime RentalDate { get; set; } = DateTime.Now;

        public DateTime ExpectedReturnDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DepositAmount { get; set; }

        public string Status { get; set; } = "Reserved";
    }
}
