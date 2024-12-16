using ClientsApi.Application.Dtos;
using ClientsApi.Application.Interfaces;
using ClientsApi.Domain;

namespace ClientsApi.Application.Services
{
    public class ClientsMapper : IClientsMapper
    {
        public ClientDto ToClientDto(Client client) => new()
        {
            Id = client.Id,
            Name = client.Name,
            Gender = client.Gender,
            Age = client.Age,
            Identification = client.Identification,
            Address = client.Address,
            PhoneNumber = client.PhoneNumber,
            Status = client.Status,
        };

        public Client ToClient(CreateClientDto dto) => new()
        {
            Name = dto.Name.Trim(),
            Gender = dto.Gender,
            Age = dto.Age,
            Identification = dto.Identification.Trim(),
            Address = dto.Address.Trim(),
            PhoneNumber = dto.PhoneNumber.Trim(),
            Password = dto.Password.Trim(),
            Status = dto.Status,
        };

        public Client ToClient(UpdateClientDto dto, Client instance)
        {
            instance.Name = dto.Name.Trim();
            instance.Gender = dto.Gender;
            instance.Age = dto.Age;
            instance.Identification = dto.Identification.Trim();
            instance.Address = dto.Address.Trim();
            instance.PhoneNumber = dto.PhoneNumber.Trim();
            instance.Password = dto.Password.Trim();
            instance.Status = dto.Status;
            return instance;
        }
    }
}
