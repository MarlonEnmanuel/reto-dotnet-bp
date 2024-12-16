using ClientsApi.Domain;

namespace ClientsApi.Infrastructure.Interfaces
{
    public interface IClientsRepository
    {
        Task<List<Client>> GetAll();
        Task<Client?> GetById(int clientId);
        Task<bool> Exists(int clientId);
        Task<bool> ExistsIdentification(string identification, int excludeId);
        Task Create(Client client);
        Task Update(Client client);
        Task<bool> Delete(int clientId);
    }
}