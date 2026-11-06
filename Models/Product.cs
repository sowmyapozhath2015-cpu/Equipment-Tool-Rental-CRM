using System.ComponentModel.DataAnnotations;

namespace RentalCRM.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        public decimal RentalPricePerDay { get; set; }

        public decimal DepositAmount { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}