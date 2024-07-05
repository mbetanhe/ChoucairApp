using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChoucairApp.Core.Application
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddServicesFromApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
