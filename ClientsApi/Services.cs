using ClientsApi.Application.Interfaces;
using ClientsApi.Application.Services;
using ClientsApi.Domain;
using ClientsApi.Domain.Validators;
using ClientsApi.Infrastructure.Interfaces;
using ClientsApi.Infrastructure.Repositories;
using FluentValidation;

namespace ClientsApi
{
    public static class Services
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientsRepository, DbClientsRepository>();

            services.AddScoped<IValidator<Client>, ClientValidator>();

            services.AddSingleton<IClientsMapper, ClientsMapper>();
            services.AddScoped<IClientsService, ClientsService>();
        }
    }
}
