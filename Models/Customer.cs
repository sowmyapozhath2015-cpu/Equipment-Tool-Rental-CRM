
using System.ComponentModel.DataAnnotations;

namespace RentalCRM.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
