using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyInfrastructure;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using CleemyInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleemyInfrastructure
{
    public static class RegisterInfrastructureExtension
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<ApplicationContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("PaymentsDb"));
                });

            services.AddScoped<IEnumerableAdapter<DbPayment, Payment>, DbPayment2PaymentAdapter>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
