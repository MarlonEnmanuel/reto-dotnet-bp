using AccountsApi.Infrastructure.Interfaces;
using FluentValidation;

namespace AccountsApi.Domain.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator(IClientsRepository clientsRepository)
        {
            RuleFor(a => a.Number)
                .NotEmpty()
                .WithMessage("El número de cuenta no debe ser vacío")
                .MaximumLength(20)
                .WithMessage("El número de cuenta no debe exceder los 20 caracteres");

            RuleFor(a => a.Type)
                .IsInEnum()
                .WithMessage("El tipo de cuenta no es válido");

            RuleFor(a => a.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El saldo debe ser mayor o igual a 0");

            RuleFor(a => a.ClientId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage("El cliente asociado a la cuenta no es válido")
                .MustAsync(async (clientId, _) => await clientsRepository.Exists(clientId))
                .WithMessage("El cliente no existe");
        }
    }
}
