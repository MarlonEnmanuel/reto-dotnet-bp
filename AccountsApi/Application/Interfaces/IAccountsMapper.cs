using AccountsApi.Application.Dtos;
using AccountsApi.Domain;

namespace AccountsApi.Application.Interfaces
{
    public interface IAccountsMapper
    {
        AccountDto ToAccountDto(Account account);
        Account ToAccount(CreateAccountDto saveAccountDto);
        Account ToAccount(UpdateAccountDto dto, Account instante);
    }
}