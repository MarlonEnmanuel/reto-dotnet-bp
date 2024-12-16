namespace AccountsApi.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountsRepository AccountsRepository { get; }
        IMovementsRepository MovementsRepository { get; }
        IClientsRepository ClientsRepository { get; }
        Task Commit();
    }
}