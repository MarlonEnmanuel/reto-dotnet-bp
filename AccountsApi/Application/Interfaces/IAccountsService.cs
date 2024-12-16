using AccountsApi.Application.Dtos;

namespace AccountsApi.Application.Interfaces
{
    public interface IAccountsService
    {
        Task<List<AccountDto>> GetAccounts();
        Task<AccountDto?> GetAccount(string accountNumber);
        Task<AccountDto> CreateAccount(CreateAccountDto dto);
        Task<AccountDto> UpdateAccount(string accountNumber, UpdateAccountDto dto);
        Task DeleteAccount(string accountNumber);
    }
}
