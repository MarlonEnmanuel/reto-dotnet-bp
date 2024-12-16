using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using AccountsApi.Domain;

namespace AccountsApi.Application.Services
{
    public class AccountsMapper : IAccountsMapper
    {
        public AccountDto ToAccountDto(Account account) => new()
        {
            Number = account.Number,
            Type = account.Type,
            Balance = account.Balance,
            Status = account.Status,
            ClientId = account.ClientId,
        };

        public Account ToAccount(CreateAccountDto dto) => new()
        {
            Number = dto.Number.Trim(),
            Type = dto.Type,
            Balance = dto.Balance,
            Status = dto.Status,
            ClientId = dto.ClientId,
        };

        public Account ToAccount(UpdateAccountDto dto, Account instante)
        {
            instante.Type = dto.Type;
            instante.Status = dto.Status;
            instante.ClientId = dto.ClientId;
            return instante;
        }
    }
}
