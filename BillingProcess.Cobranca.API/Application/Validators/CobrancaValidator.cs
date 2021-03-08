using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobranca.API.Application.Validators
{
    public class CobrancaValidator : AbstractValidator<Models.Cobranca>
    {
        public CobrancaValidator()
        {
            RuleFor(cobranca => cobranca.DataVencimento).NotEmpty().NotNull().WithMessage("Nome não pode ser vazio");
            RuleFor(cobranca => cobranca.Valor).NotEmpty().NotNull().WithMessage("Valor não pode ser vazio");
            RuleFor(cobranca => cobranca.Cliente).NotEmpty().NotNull().WithMessage("Cliente não pode ser vazio");
        }
    }
}
