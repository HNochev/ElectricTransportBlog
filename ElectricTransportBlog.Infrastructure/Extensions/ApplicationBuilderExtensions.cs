using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            SeedTransportNetworkTypes(services);

            return app;
        }

        private static void SeedTransportNetworkTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.LineTypes.Any())
            {
                return;
            }

            data.LineTypes.AddRange(new[]
            {
                new LineType { TypeOfVehicle = "Трамвай", ClassColor = "table-danger", Counter = 1},
                new LineType { TypeOfVehicle = "Тролейбус", ClassColor = "table-primary", Counter = 2},
                new LineType { TypeOfVehicle = "Електробус", ClassColor = "table-warning", Counter = 3},
            });

            data.SaveChanges();
        }
    }
}
