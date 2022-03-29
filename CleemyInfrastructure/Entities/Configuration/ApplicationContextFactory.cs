using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleemyInfrastructure.Entities.Configuration
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost; User ID=sa;Password=XNc7PA5nxxW8ny; Initial Catalog=Cleemy;Application Name=CleemyApi;");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}