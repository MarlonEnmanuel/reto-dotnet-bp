using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using AccountsApi.Domain;

namespace AccountsApi.Application.Services
{
    public class MovementsMapper : IMovementsMapper
    {
        public MovementDto ToMovementDto(Movement movement) => new()
        {
            Id = movement.Id,
            AccountNumber = movement.AccountNumber,
            DateTime = movement.DateTime,
            Amount = movement.Amount,
            Balance = movement.Balance,
            Description = movement.Description,
        };
    }
}
