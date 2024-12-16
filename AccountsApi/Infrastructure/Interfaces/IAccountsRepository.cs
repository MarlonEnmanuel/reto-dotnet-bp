using AccountsApi.Domain;
using System.Linq.Expressions;

namespace AccountsApi.Infrastructure.Interfaces
{
    public interface IAccountsRepository
    {
        Task<List<Account>> GetAll();
        Task<List<Account>> Search(Expression<Func<Account, bool>> filter);
        Task<Account?> GetByNumber(string accountNumber);
        Task<bool> Exists(string accountNumber);
        Task Create(Account account);
        Task Update(Account account);
        Task<bool> Delete(string accountNumber);
    }
}
