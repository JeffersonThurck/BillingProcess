using BillingProcess.Client.API.Dados.Repository;
using BillingProcess.Client.API.Application.Validators;
using BillingProcess.Client.API.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BillingProcess.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();

            services.AddSingleton<IValidator<Cliente>, ClienteValidator>();
        }
    }
}
