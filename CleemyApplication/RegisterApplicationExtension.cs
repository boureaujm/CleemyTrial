using CleemyApplication.Services;
using CleemyApplication.Services.internals;
using Microsoft.Extensions.DependencyInjection;

namespace CleemyApplication
{
    public static class RegisterApplicationExtension
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}