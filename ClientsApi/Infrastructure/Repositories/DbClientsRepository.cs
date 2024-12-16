using ClientsApi.Domain;
using ClientsApi.Infrastructure.Database;
using ClientsApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientsApi.Infrastructure.Repositories
{
    public class DbClientsRepository(ClientsContext context) : IClientsRepository
    {
        public async Task<List<Client>> GetAll()
        {
            return await context.Clients
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Client?> GetById(int clientId)
        {
            return await context.Clients
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<bool> Exists(int clientId)
        {
            return await context.Clients.AnyAsync(c => c.Id == clientId);
        }

        public async Task<bool> ExistsIdentification(string identification, int excludeId)
        {
            return await context.Clients.AnyAsync(c => c.Identification == identification && c.Id != excludeId);
        }

        public async Task Create(Client client)
        {
            if (client.Id != default)
                throw new InvalidOperationException($"El cliente tiene ID asignado");

            await context.Clients.AddAsync(client);
        }

        public async Task Update(Client client)
        {
            var currentClient = await context.Clients.FindAsync(client.Id) ??
                throw new InvalidOperationException($"El cliente '{client.Id}' no existe");

            context.Entry(currentClient).CurrentValues.SetValues(client);
        }

        public async Task<bool> Delete(int clientId)
        {
            var client = await context.Clients.FindAsync(clientId);
            if (client == null) return false;

            context.Clients.Remove(client);
            return true;
        }
    }
}
