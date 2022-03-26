using Cleemy.ActionFilters;
using Cleemy.DTO;
using Cleemy.Model.Adapter;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Cleemy.Configuration
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<ValidateModelStateAttribute<CreatePaymentDto>>();
            services.AddScoped<ValidateModelStateAttribute<SortWrapperDto>>();

            services.AddScoped<IEnumerableAdapter<CreatePaymentDto, Payment>, CreatePaymentDtoToPaymentAdapter>();
            services.AddScoped<IEnumerableAdapter<Payment, PaymentDto>, PaymentToPaymentDtoAdapter>();
            services.AddScoped<IAdapter<SortWrapperDto, SortWrapper>, SortWrapperDtoToSortWrapperAdapter>();
            

            System.Reflection.Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(item => item.GetInterfaces()
           .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IValidator<>)) && !item.IsAbstract && !item.IsInterface)
           .ToList()
           .ForEach(assignedTypes =>
           {
               var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IValidator<>));
               services.AddScoped(serviceType, assignedTypes);
           });
        }
    }
}