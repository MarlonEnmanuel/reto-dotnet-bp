using AccountsApi.Application.Dtos;
using FluentValidation;

namespace AccountsApi.Application
{
    public class ReportQueryDtoValidator : AbstractValidator<GenerateReportDto>
    {
        public ReportQueryDtoValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .WithMessage("El cliente es requerido");

            RuleFor(x => x.StartDate)
                //.NotNull() //TODO
                //.WithMessage("La fecha de inicio es requerida")
                .Must(x => x!.TimeOfDay == new TimeSpan(0, 0, 0))
                .WithMessage("Debe ingresar solo la fecha de inicio sin hora");

            RuleFor(x => x.EndDate)
                //.NotNull()
                //.WithMessage("La fecha de fin es requerida")
                .Must(x => x!.TimeOfDay == new TimeSpan(0, 0, 0))
                .WithMessage("Debe ingresar solo la fecha de fin sin hora")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("La fecha de fin debe ser mayor o igual a la fecha de inicio");
        }
    }
}
