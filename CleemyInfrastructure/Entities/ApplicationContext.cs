using CleemyInfrastructure.entities;
using Microsoft.EntityFrameworkCore;

namespace CleemyInfrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<DbCurrency> Currencies { get; set; }
        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbPayment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Cleemy");

            modelBuilder.Entity<DbCurrency>().HasData(
                new DbCurrency
                {
                    Code = "USD",
                    Label = "US Dollar"
                },
                 new DbCurrency
                 {
                     Code = "RUB",
                     Label = "Russian Ruble"
                 }
            );

            modelBuilder.Entity<DbUser>().HasData(
                new DbUser
                {
                    FirstName = "Stark",
                    LastName = "Anthony",
                    Id = 1,
                    AuthorizedCurrencyCode = "USD"
                },
                new DbUser
                {
                    FirstName = "Romanova",
                    LastName = "Natasha",
                    Id = 2,
                    AuthorizedCurrencyCode = "RUB"
                }
            );
        }
    }
}