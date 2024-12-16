using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using AccountsApi.Infrastructure.Interfaces;
using MovementsApi.Application.Interfaces;
using Shared.Exceptions;

namespace AccountsApi.Application.Services
{
    public class MovementsService(IUnitOfWork unitOfWork,
                                  IMovementsMapper mapper) : IMovementsService
    {
        public async Task<List<MovementDto>> GetMovements()
        {
            var movements = await unitOfWork.MovementsRepository.GetAll();
            return movements.Select(mapper.ToMovementDto).ToList();
        }

        public async Task<MovementDto?> GetMovement(int movementId)
        {
            var movement = await unitOfWork.MovementsRepository.GetById(movementId) ??
                throw new NotFoundException($"El movimiento '{movementId}' no existe");

            return mapper.ToMovementDto(movement);
        }

        public async Task<MovementDto> CreateMovement(CreateMovementDto dto)
        {
            var account = await unitOfWork.AccountsRepository.GetByNumber(dto.AccountNumber) ??
                throw new NotFoundException($"La cuenta '{dto.AccountNumber}' no existe");

            var movement = account.CreateMovement(dto.Amount);
            account.ValidateBalance();

            await unitOfWork.AccountsRepository.Update(account);
            await unitOfWork.MovementsRepository.Create(movement);
            await unitOfWork.Commit();

            return mapper.ToMovementDto(movement);
        }

        public async Task<List<MovementDto>> UpdateMovement(int movementId, UpdateMovementDto dto)
        {
            if (movementId <= 0 || movementId != dto.Id)
                throw new BadRequestException("El Id del movimiento es incorrecto");

            var movement = await unitOfWork.MovementsRepository.GetById(dto.Id) ??
                throw new NotFoundException($"El movimiento '{dto.Id}' no existe");

            if (movement.IsReversion)
                throw new BadRequestException("El movimiento no se puede modificar ya que es de anulación");

            var account = await unitOfWork.AccountsRepository.GetByNumber(movement.AccountNumber) ??
                throw new NotFoundException($"No se encontró la cuenta asociada al movimiento");

            var reverseMovement = account.ReverseMovement(movement);
            var newMovement = account.CreateMovement(dto.Amount);
            account.ValidateBalance();

            await unitOfWork.AccountsRepository.Update(account);
            await unitOfWork.MovementsRepository.Update(movement);
            await unitOfWork.MovementsRepository.Create(reverseMovement);
            await unitOfWork.MovementsRepository.Create(newMovement);
            await unitOfWork.Commit();

            return
            [
                mapper.ToMovementDto(reverseMovement),
                mapper.ToMovementDto(newMovement)
            ];
        }

        public async Task<MovementDto> DeleteMovement(int movementId)
        {
            var movement = await unitOfWork.MovementsRepository.GetById(movementId);
            if (movement == null)
                throw new NotFoundException($"El movimiento '{movementId}' no existe");

            if (movement.IsReversion)
                throw new BadRequestException("El movimiento no se puede eliminar ya que es de anulación");

            var account = await unitOfWork.AccountsRepository.GetByNumber(movement.AccountNumber) ??
                throw new NotFoundException($"No se encontró la cuenta asociada al movimiento");

            var newMovement = account.ReverseMovement(movement);
            account.ValidateBalance();

            await unitOfWork.AccountsRepository.Update(account);
            await unitOfWork.MovementsRepository.Update(movement);
            await unitOfWork.MovementsRepository.Create(newMovement);
            await unitOfWork.Commit();

            return mapper.ToMovementDto(newMovement);
        }
    }
}
