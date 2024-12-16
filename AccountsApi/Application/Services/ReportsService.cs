using AccountsApi.Application.Dtos;
using AccountsApi.Infrastructure.Interfaces;
using Shared.Exceptions;
using Shared.Enums;
using AccountsApi.Domain;
using AccountsApi.Application.Interfaces;
using FluentValidation;

namespace AccountsApi.Application.Services
{
    public class ReportsService(IUnitOfWork unitOfWork,
                                IValidator<GenerateReportDto> validator) : IReportsService
    {
        public async Task<ReportDto> GetReport(GenerateReportDto dto)
        {
            await validator.ValidateAndThrowAsync(dto);

            var client = await unitOfWork.ClientsRepository.GetById(dto.ClientId) ??
                throw new BadRequestException("Cliente no encontrado");

            var movements = await unitOfWork.MovementsRepository
                                            .Search(m => m.Account.ClientId == dto.ClientId &&
                                                         m.DateTime >= dto.StartDate &&
                                                         m.DateTime < dto.EndDate.AddDays(1));

            return new ReportDto
            {
                StartDate = dto.StartDate.ToShortDateString(),
                EndDate = dto.EndDate.ToShortDateString(),
                ClientIdentification = client.Identification,
                ClientName = client.Name,
                Accounts = ToReportAccountDto(movements),
            };
        }

        private List<ReportAccountDto> ToReportAccountDto(List<Movement> movements)
        {
            return movements
                    .GroupBy(m => m.AccountNumber)
                    .Select(g => new ReportAccountDto
                    {
                        AccountNumber = g.First().Account.Number,
                        AccountType = g.First().Account.Type.GetDescription(),
                        Status = g.First().Account.Status,
                        Movements = ToReportMovementDto(g.ToList()),
                    })
                    .ToList();
        }

        private List<ReportMovementDto> ToReportMovementDto(List<Movement> movement)
        {
            var result = new List<ReportMovementDto>();
            var orderedMovements = movement.OrderBy(m => m.Id).ToList();

            result.Add(new ReportMovementDto()
            {
                Amount = orderedMovements.First().InitialBalance,
                Description = "Saldo inicial"
            });

            result.AddRange(orderedMovements.Select(m => new ReportMovementDto
            {
                MovementId = m.Id,
                Amount = m.Amount,
                Description = m.Description
            }));

            result.Add(new ReportMovementDto()
            {
                Amount = orderedMovements.Last().Balance,
                Description = "Saldo final"
            });

            return result;
        }
    }
}
