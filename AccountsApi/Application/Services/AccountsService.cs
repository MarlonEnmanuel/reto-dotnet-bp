using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using AccountsApi.Domain;
using AccountsApi.Infrastructure.Interfaces;
using FluentValidation;
using Shared.Exceptions;

namespace AccountsApi.Application.Services
{
    public class AccountsService(IUnitOfWork unitOfWork,
                                 IAccountsMapper mapper,
                                 IValidator<Account> validator) : IAccountsService
    {
        public async Task<List<AccountDto>> GetAccounts()
        {
            var accounts = await unitOfWork.AccountsRepository.GetAll();
            return accounts.Select(mapper.ToAccountDto).ToList();
        }

        public async Task<AccountDto?> GetAccount(string accountNumber)
        {
            var account = await unitOfWork.AccountsRepository.GetByNumber(accountNumber) ??
                throw new NotFoundException($"La cuenta '{accountNumber}' no existe");

            return mapper.ToAccountDto(account);
        }

        public async Task<AccountDto> CreateAccount(CreateAccountDto dto)
        {
            var exists = await unitOfWork.AccountsRepository.Exists(dto.Number);
            if (exists)
                throw new BadRequestException($"El cuenta {dto.Number} ya existe");

            var account = mapper.ToAccount(dto);
            await validator.ValidateAndThrowAsync(account);

            await unitOfWork.AccountsRepository.Create(account);
            await unitOfWork.Commit();

            return mapper.ToAccountDto(account);
        }

        public async Task<AccountDto> UpdateAccount(string accountNumber, UpdateAccountDto dto)
        {
            if (accountNumber != dto.Number)
                throw new BadRequestException("El número de cuenta es incorrecto");

            var account = await unitOfWork.AccountsRepository.GetByNumber(dto.Number);
            if (account == null)
                throw new BadRequestException($"La cuenta {dto.Number} no existe");

            mapper.ToAccount(dto, account);
            await validator.ValidateAndThrowAsync(account);

            await unitOfWork.AccountsRepository.Update(account);
            await unitOfWork.Commit();

            return mapper.ToAccountDto(account);
        }

        public async Task DeleteAccount(string accountNumber)
        {
            var exists = await unitOfWork.AccountsRepository.Exists(accountNumber);
            if (!exists)
                throw new NotFoundException($"La cuenta '{accountNumber}' no existe");

            await unitOfWork.AccountsRepository.Delete(accountNumber);
            await unitOfWork.Commit();
        }
    }
}
