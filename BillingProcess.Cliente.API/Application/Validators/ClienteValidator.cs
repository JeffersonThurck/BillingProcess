using BillingProcess.Client.API.Models;
using FluentValidation;

namespace BillingProcess.Client.API.Application.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome).NotEmpty().NotNull().WithMessage("Nome não pode ser vazio");
            RuleFor(cliente => cliente.CPF).NotEmpty().NotNull().Matches(@"[0-9]{3}\?[0-9]{3}\?[0-9]{3}\?[0-9]{2}")
                .MaximumLength(11)
                .WithMessage("O campo Cpf não pode ser vazio ou tamanho inválido");
            RuleFor(cliente => cliente.Estado).NotEmpty().NotNull().WithMessage("O campo Estado não pode ser vazio");
        }
    }
}
