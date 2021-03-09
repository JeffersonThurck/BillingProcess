using BillingProcess.Cobrancas.API.Application.Validators;
using BillingProcess.Cobrancas.API.Data.Repository;
using BillingProcess.Cobrancas.API.Models;
using BillingProcess.Cobrancas.API.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobrancas.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICobrancaRepository, CobrancaRepository>();

            services.AddSingleton<IValidator<Cobranca>, CobrancaValidator>();

            services.AddHttpClient<ICobrancaService, CobrancaService>();
        }
    }
}
