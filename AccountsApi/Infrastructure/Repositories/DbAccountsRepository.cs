using AccountsApi.Domain;
using AccountsApi.Infrastructure.Database;
using AccountsApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountsApi.Infrastructure.Repositories
{
    public class DbAccountsRepository(AccountsContext context) : IAccountsRepository
    {
        public async Task<List<Account>> GetAll()
        {
            return await context.Accounts
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<List<Account>> Search(Expression<Func<Account, bool>> filter)
        {
            return await context.Accounts
                                .AsNoTracking()
                                .Include(a => a.Movements)
                                .Where(filter)
                                .ToListAsync();
        }

        public async Task<Account?> GetByNumber(string accountNumber)
        {
            return await context.Accounts
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Number == accountNumber);
        }

        public async Task<bool> Exists(string accountNumber)
        {
            return await context.Accounts.AnyAsync(c => c.Number == accountNumber);
        }

        public async Task Create(Account account)
        {
            if (await Exists(account.Number))
                throw new InvalidOperationException($"La cuenta '{account.Number}' ya existe");

            await context.Accounts.AddAsync(account);
        }

        public async Task Update(Account account)
        {
            var currentAccount = await context.Accounts.FindAsync(account.Number) ??
                throw new InvalidOperationException($"La cuenta '{account.Number}' no existe");

            context.Entry(currentAccount).CurrentValues.SetValues(account);
        }

        public async Task<bool> Delete(string accountNumber)
        {
            var account = await context.Accounts.FindAsync(accountNumber);
            if (account == null) return false;

            context.Accounts.Remove(account);
            return true;
        }
    }
}
