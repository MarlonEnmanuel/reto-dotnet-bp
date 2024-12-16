using ClientsApi.Infrastructure.Database;
using ClientsApi.Infrastructure.Interfaces;

namespace ClientsApi.Infrastructure.Repositories
{
    public class UnitOfWork(ClientsContext context,
                            IClientsRepository clientsRepository) : IUnitOfWork
    {
        public IClientsRepository ClientsRepository { get; } = clientsRepository;

        public async Task Commit()
            => await context.SaveChangesAsync();

        public async Task Rollback()
            => await context.DisposeAsync();

        public void Dispose()
            => context.Dispose();
    }
}
