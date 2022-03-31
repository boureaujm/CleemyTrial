using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleemyInfrastructure
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationContext>();
            context.Database.Migrate();
        }
    }
}