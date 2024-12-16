using ClientsApi.Application.Dtos;
using ClientsApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientsApi.Infrastructure.Controllers
{
    public static class ClientsController
    {
        public static void MapClientRoutes(this IEndpointRouteBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("/clients").WithOpenApi();

            group.MapGet("", async ([FromServices] IClientsService service) =>
            {
                return Results.Ok(await service.GetClients());
            });

            group.MapGet("{id:int}", async (int id, [FromServices] IClientsService service) =>
            {
                return Results.Ok(await service.GetClient(id));
            });

            group.MapPost("", async ([FromBody] CreateClientDto dto, [FromServices] IClientsService service) =>
            {
                var result = await service.CreateClient(dto);
                return Results.Created($"/clients/{result.Id}", result);
            });

            group.MapPut("{id:int}", async (int id, [FromBody] UpdateClientDto dto, [FromServices] IClientsService service) =>
            {
                var result = await service.UpdateClient(id, dto);
                return Results.Accepted($"/clients/{result.Id}", result);
            });

            group.MapDelete("{id:int}", async (int id, [FromServices] IClientsService service) =>
            {
                await service.DeleteClient(id);
                return Results.NoContent();
            });
        }
    }
}
