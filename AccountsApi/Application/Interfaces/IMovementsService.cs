using AccountsApi.Application.Dtos;

namespace MovementsApi.Application.Interfaces
{
    public interface IMovementsService
    {
        Task<List<MovementDto>> GetMovements();
        Task<MovementDto?> GetMovement(int movementId);
        Task<MovementDto> CreateMovement(CreateMovementDto dto);
        Task<List<MovementDto>> UpdateMovement(int movementId, UpdateMovementDto dto);
        Task<MovementDto> DeleteMovement(int movementId);
    }
}
