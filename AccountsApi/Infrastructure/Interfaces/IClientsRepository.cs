using AccountsApi.Domain;

namespace AccountsApi.Infrastructure.Interfaces
{
    public interface IClientsRepository
    {
        void Dispose();
        Task<Client?> GetById(int clientId);
        Task<bool> Exists(int clientId);
    }
}