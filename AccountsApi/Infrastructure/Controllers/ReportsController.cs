using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.Infrastructure.Controllers
{
    public static class ReportsController
    {
        public static void MapReportsRoutes(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/reports", async ([AsParameters] GenerateReportDto query, [FromServices]IReportsService service) =>
            {
                return Results.Ok(await service.GetReport(query));
            });
        }
    }
}
