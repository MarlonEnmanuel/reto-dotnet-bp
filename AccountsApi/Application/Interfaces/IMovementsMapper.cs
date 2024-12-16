using AccountsApi.Application.Dtos;
using AccountsApi.Domain;

namespace AccountsApi.Application.Interfaces
{
    public interface IMovementsMapper
    {
        MovementDto ToMovementDto(Movement movement);
    }
}