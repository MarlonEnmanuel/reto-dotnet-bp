using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using AccountsApi.Application.Services;
using AccountsApi.Application.Validators;
using AccountsApi.Domain;
using AccountsApi.Domain.Validators;
using AccountsApi.Infrastructure.Interfaces;
using AccountsApi.Infrastructure.Repositories;
using ClientsApi.Infrastructure.Repositories;
using FluentValidation;
using MovementsApi.Application.Interfaces;
using Shared.Models;

namespace AccountsApi
{
    public static class Services
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddOptions<ClientsApiOptions>().BindConfiguration("ClientsApi");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountsRepository, DbAccountsRepository>();
            services.AddScoped<IMovementsRepository, DbMovementsRepository>();
            services.AddSingleton<IClientsRepository, ApiClientsRepository>();

            services.AddScoped<IValidator<Account>, AccountValidator>();
            services.AddScoped<IValidator<GenerateReportDto>, ReportQueryDtoValidator>();

            services.AddSingleton<IAccountsMapper, AccountsMapper>();
            services.AddSingleton<IMovementsMapper, MovementsMapper>();

            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IMovementsService, MovementsService>();
            services.AddScoped<IReportsService, ReportsService>();
        }
    }
}
