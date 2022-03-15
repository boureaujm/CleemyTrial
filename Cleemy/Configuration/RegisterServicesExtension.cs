using Cleemy.DTO;
using Cleemy.Model.Adapter;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Cleemy.Configuration
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services) {
            services.AddScoped<IEnumerableAdapter<Payment, PaymentDto>, PayementAdapter>();
        }
    }
}
