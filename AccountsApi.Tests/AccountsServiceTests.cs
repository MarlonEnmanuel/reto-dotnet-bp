using AccountsApi.Application.Dtos;
using AccountsApi.Application.Services;
using AccountsApi.Domain;
using AccountsApi.Infrastructure.Interfaces;
using FluentAssertions;
using FluentValidation;
using Moq;
using Shared.Exceptions;

namespace AccountsApi.Tests
{
    public class AccountsServiceTests
    {
        private readonly AccountsService _service;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<Account>> _validatorMock;

        public AccountsServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<Account>>();
            _service = new AccountsService(_unitOfWorkMock.Object,
                                           new AccountsMapper(),
                                           _validatorMock.Object);
        }

        [Fact]
        public void CreateAccount_WhenAccountExists_ThrowsBadRequestException()
        {
            var dto = new CreateAccountDto { Number = "1234" };
            _unitOfWorkMock.Setup(u => u.AccountsRepository.Exists(dto.Number)).ReturnsAsync(true);

            Func<Task> act = async () => await _service.CreateAccount(dto);

            act.Should().ThrowAsync<BadRequestException>().WithMessage("El cuenta 1234 ya existe");
            _unitOfWorkMock.Verify(u => u.AccountsRepository.Exists(dto.Number), Times.Once);
        }

        [Fact]
        public async Task CreateAccount_WhenAccountDoesNotExist_CreatesAccount()
        {
            var dto = new CreateAccountDto { Number = "1234" };
            
            _unitOfWorkMock.Setup(u => u.AccountsRepository.Exists(dto.Number)).ReturnsAsync(false);
            _unitOfWorkMock.Setup(u => u.AccountsRepository.Create(It.IsAny<Account>()));
            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), default));

            var result = await _service.CreateAccount(dto);

            result.Number.Should().Be(dto.Number);
            _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<IValidationContext>(), default), Times.Once);
            _unitOfWorkMock.Verify(u => u.AccountsRepository.Create(It.IsAny<Account>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void UpdateAccount_WhenAccountDoesNotExist_ThrowsBadRequestException()
        {
            var dto = new UpdateAccountDto { Number = "1234" };
            _unitOfWorkMock.Setup(u => u.AccountsRepository.GetByNumber(dto.Number)).ReturnsAsync((Account?)null);

            Func<Task> act = async () => await _service.UpdateAccount(dto.Number, dto);

            act.Should().ThrowAsync<BadRequestException>().WithMessage("La cuenta 1234 no existe");
            _unitOfWorkMock.Verify(u => u.AccountsRepository.GetByNumber(dto.Number), Times.Once);
        }

        [Fact]
        public async Task UpdateAccount_WhenAccountExists_UpdatesAccount()
        {
            var dto = new UpdateAccountDto { Number = "1234" };
            var account = new Account { Number = dto.Number };
            _unitOfWorkMock.Setup(u => u.AccountsRepository.GetByNumber(dto.Number)).ReturnsAsync(account);
            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), default));

            var result = await _service.UpdateAccount(dto.Number, dto);

            result.Number.Should().Be(dto.Number);
            _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<IValidationContext>(), default), Times.Once);
            _unitOfWorkMock.Verify(u => u.AccountsRepository.Update(account), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
