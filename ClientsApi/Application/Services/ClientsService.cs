using ClientsApi.Application.Dtos;
using ClientsApi.Application.Interfaces;
using ClientsApi.Domain;
using ClientsApi.Infrastructure.Interfaces;
using FluentValidation;
using Shared.Exceptions;

namespace ClientsApi.Application.Services
{
    public class ClientsService(IUnitOfWork unitOfWork,
                                IClientsMapper mapper,
                                IValidator<Client> validator) : IClientsService
    {
        public async Task<List<ClientDto>> GetClients()
        {
            var clients = await unitOfWork.ClientsRepository.GetAll();
            return clients.Select(mapper.ToClientDto).ToList();
        }

        public async Task<ClientDto?> GetClient(int clientId)
        {
            var client = await unitOfWork.ClientsRepository.GetById(clientId) ??
                throw new NotFoundException($"El cliente con id '{clientId}' no existe");

            return mapper.ToClientDto(client);
        }

        public async Task<ClientDto> CreateClient(CreateClientDto dto)
        {
            var client = mapper.ToClient(dto);
            await validator.ValidateAndThrowAsync(client);

            await unitOfWork.ClientsRepository.Create(client);
            await unitOfWork.Commit();

            return mapper.ToClientDto(client);
        }

        public async Task<ClientDto> UpdateClient(int clientId, UpdateClientDto dto)
        {
            if (clientId <= 0 || clientId != dto.Id)
                throw new BadRequestException("El Id del cliente es incorrecto");

            var client = await unitOfWork.ClientsRepository.GetById(clientId);
            if (client == null)
                throw new NotFoundException($"El cliente con id '{clientId}' no existe");

            mapper.ToClient(dto, client);
            await validator.ValidateAndThrowAsync(client);

            await unitOfWork.ClientsRepository.Update(client);
            await unitOfWork.Commit();

            return mapper.ToClientDto(client);
        }

        public async Task DeleteClient(int clientId)
        {
            var exists = await unitOfWork.ClientsRepository.Exists(clientId);
            if (!exists)
                throw new NotFoundException($"El cliente con id '{clientId}' no existe");

            await unitOfWork.ClientsRepository.Delete(clientId);
            await unitOfWork.Commit();
        }
    }
}
