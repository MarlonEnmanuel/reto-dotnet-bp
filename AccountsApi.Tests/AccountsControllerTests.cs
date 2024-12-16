using AccountsApi.Application.Dtos;
using AccountsApi.Domain;
using AccountsApi.Infrastructure.Database;
using AccountsApi.Infrastructure.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Enums;
using Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace AccountsApi.Tests
{
    public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly AccountsContext _context;

        private Client? _clientMock = new() { Id = 1, Name = "Jhon Doe" };

        public AccountsControllerTests(WebApplicationFactory<Program> factory)
        {
            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // remove db configuration
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AccountsContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    // replace db context
                    services.AddDbContext<AccountsContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestAccountsDb");
                    });

                    // mock IClientsRepository
                    services.AddScoped(provider =>
                    {
                        var mock = new Mock<IClientsRepository>();
                        mock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(() => _clientMock);
                        mock.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(() => _clientMock != null);
                        return mock.Object;
                    });
                });
            });

            _client = factory.CreateClient();

            _context = factory.Services.CreateScope()
                                       .ServiceProvider
                                       .GetRequiredService<AccountsContext>();
        }

        [Fact]
        public async Task GetAccount_ShouldGetAccount()
        {
            _context.Accounts.Add(new() { Number = "001", Type = AccountType.Checking, Balance = 100, Status = true, ClientId = 1 });
            _context.SaveChanges();

            var response = await _client.GetAsync("/accounts/001");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var accountDto = await response.Content.ReadFromJsonAsync<AccountDto?>();
            accountDto.Should().NotBeNull();
            accountDto!.Number.Should().Be("001");
            accountDto!.Type.Should().Be(AccountType.Checking);
            accountDto!.Balance.Should().Be(100);
            accountDto!.Status.Should().BeTrue();
            accountDto!.ClientId.Should().Be(1);
        }

        [Fact]
        public async Task PostAccount_ShouldSaveInDb()
        {
            var createAccountDto = new CreateAccountDto
            {
                Number = "001",
                Type = AccountType.Checking,
                Balance = 150,
                Status = true,
                ClientId = 1
            };

            var response = await _client.PostAsJsonAsync("/accounts", createAccountDto);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var accountDto = await response.Content.ReadFromJsonAsync<AccountDto?>();
            accountDto.Should().NotBeNull();
            accountDto!.Number.Should().Be("001");

            var account = await _context.Accounts.FindAsync("001");
            account.Should().NotBeNull();
            account!.Number.Should().Be("001");
            account!.Type.Should().Be(AccountType.Checking);
            account!.Balance.Should().Be(150);
            account!.Status.Should().BeTrue();
            account!.ClientId.Should().Be(1);
        }

        [Fact]
        public async Task PostAccount_ShouldReturnErrorIfClientDoesntExists()
        {
            _clientMock = null;
            var createAccountDto = new CreateAccountDto
            {
                Number = "001",
                Type = AccountType.Checking,
                Balance = 150,
                Status = true,
                ClientId = 1
            };

            var response = await _client.PostAsJsonAsync("/accounts", createAccountDto);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadFromJsonAsync<ErrorResponse?>();
            result.Should().NotBeNull();
            result!.Error.Should().Be("Algunos datos son incorrectos");
            result!.ErrorDetails.Should().NotBeNullOrEmpty();
            result!.ErrorDetails!.Any(d => d.Contains("cliente no existe")).Should().BeTrue();
        }

        public void Dispose()
        {
            _context.Accounts.RemoveRange(_context.Accounts);
            _context.SaveChanges();
        }
    }
}