using ClientsApi.Application.Dtos;

namespace ClientsApi.Application.Interfaces
{
    public interface IClientsService
    {
        Task<List<ClientDto>> GetClients();
        Task<ClientDto?> GetClient(int clientId);
        Task<ClientDto> CreateClient(CreateClientDto dto);
        Task<ClientDto> UpdateClient(int clientId, UpdateClientDto dto);
        Task DeleteClient(int clientId);
    }
}