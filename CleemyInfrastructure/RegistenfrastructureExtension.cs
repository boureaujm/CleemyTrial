using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using Microsoft.Extensions.DependencyInjection;

namespace CleemyApplication
{
    public static class RegistenfrastructureExtension
    {
        public static void RegisterServices(this IServiceCollection services) {
            services.AddScoped<IEnumerableAdapter<DbPayment, Payment>, DbPaymentAdapter>();
        }
    }
}
