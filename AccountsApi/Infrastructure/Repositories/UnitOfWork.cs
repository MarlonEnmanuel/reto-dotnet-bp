using AccountsApi.Infrastructure.Database;
using AccountsApi.Infrastructure.Interfaces;

namespace ClientsApi.Infrastructure.Repositories
{
    public class UnitOfWork(AccountsContext context,
                            IAccountsRepository accountsRepository,
                            IMovementsRepository movementsRepository,
                            IClientsRepository clientsRepository) : IUnitOfWork
    {
        public IAccountsRepository AccountsRepository { get; } = accountsRepository;
        public IMovementsRepository MovementsRepository { get; } = movementsRepository;
        public IClientsRepository ClientsRepository { get; } = clientsRepository;


        public async Task Commit()
            => await context.SaveChangesAsync();

        public async Task Rollback()
            => await context.DisposeAsync();

        public void Dispose()
            => context.Dispose();
    }
}
