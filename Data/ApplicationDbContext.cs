
using Microsoft.EntityFrameworkCore;
using RentalCRM.Models;

namespace RentalCRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<RentalItem> RentalItems { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
