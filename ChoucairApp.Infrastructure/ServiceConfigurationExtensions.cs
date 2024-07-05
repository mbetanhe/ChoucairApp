using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChoucairApp.Infrastructure
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddServicesFromInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChoucairDbContext>(options =>
            options.UseSqlServer(
                //configuration.GetConnectionString("DefaultConnection"),
                "Server=.;Database=ChoucairBD;TrustServerCertificate=True;Trusted_Connection=True; MultipleActiveResultSets=True",
                b => b.MigrationsAssembly(typeof(ChoucairDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ChoucairDbContext>());

            //services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
        }

        public static void AddSeedBdFromInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
        }


    }
}
