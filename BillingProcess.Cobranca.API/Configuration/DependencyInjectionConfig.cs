using BillingProcess.Cobranca.API.Application.Validators;
using BillingProcess.Cobranca.API.Data.Repository;
using BillingProcess.Cobranca.API.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobranca.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICobrancaRepository, CobrancaRepository>();

            services.AddSingleton<IValidator<Models.Cobranca>, CobrancaValidator>();
        }
    }
}
