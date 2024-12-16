namespace ClientsApi.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientsRepository ClientsRepository { get; }
        Task Commit();
    }
}