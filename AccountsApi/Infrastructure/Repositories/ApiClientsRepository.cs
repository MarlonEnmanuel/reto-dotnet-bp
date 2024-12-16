using AccountsApi.Domain;
using AccountsApi.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Shared.Exceptions;
using Shared.Models;
using System.Net;
using System.Text.Json;

namespace AccountsApi.Infrastructure.Repositories
{
    public class ApiClientsRepository(IOptions<ClientsApiOptions> apiOptions) : IClientsRepository, IDisposable
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri(apiOptions.Value.BaseUrl),
            Timeout = TimeSpan.FromMilliseconds(apiOptions.Value.TimeoutMs),
        };

        public async Task<Client?> GetById(int clientId)
        {
            var response = await httpClient.GetAsync($"clients/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Client>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            throw new InternalException("Error al conectarse con el API de clientes");
        }

        public async Task<bool> Exists(int clientId)
        {
            return await GetById(clientId) != null;
        }

        public void Dispose()
            => httpClient.Dispose();
    }
}
