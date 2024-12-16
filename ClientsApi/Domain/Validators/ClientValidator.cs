using ClientsApi.Infrastructure.Interfaces;
using FluentValidation;

namespace ClientsApi.Domain.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator(IClientsRepository clientsRepository)
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("El nombre no debe ser vacío")
                .MaximumLength(100)
                .WithMessage("El nombre no debe exceder los 100 caracteres");

            RuleFor(d => d.Gender)
                .IsInEnum()
                .WithMessage("El género no es válido");

            RuleFor(d => d.Age)
                .GreaterThan((byte)0)
                .WithMessage("La edad debe ser mayor a 0")
                .LessThan((byte)120)
                .WithMessage("La edad no debe exceder los 120 años");

            RuleFor(d => d.Identification)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("La identificación no debe ser vacía")
                .MaximumLength(20)
                .WithMessage("La identificación no debe exceder los 20 caracteres")
                .MustAsync(async (client, identification, _)
                    => !await clientsRepository.ExistsIdentification(identification, client.Id))
                .WithMessage("La identificación no está disponible");

            RuleFor(d => d.Address)
                .NotEmpty()
                .WithMessage("La dirección no debe ser vacía")
                .MaximumLength(200)
                .WithMessage("La dirección no debe exceder los 200 caracteres");

            RuleFor(d => d.PhoneNumber)
                .NotEmpty()
                .WithMessage("El número de teléfono no debe ser vacío")
                .MaximumLength(20)
                .WithMessage("El número de teléfono no debe exceder los 20 caracteres");

            RuleFor(d => d.Password)
                .NotEmpty()
                .WithMessage("La contraseña no debe ser vacía")
                .MaximumLength(100)
                .WithMessage("La contraseña no debe exceder los 100 caracteres");
        }
    }
}
