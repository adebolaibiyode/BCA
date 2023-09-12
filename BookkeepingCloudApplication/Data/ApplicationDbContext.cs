using BookkeepingCloudApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookkeepingCloudApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Data table containing all invoices
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Registers the tables with the model
        /// </summary>
        /// <param name="modelBuilder">The model to have the database tables applied to.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method first.

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();
        }
    }
}