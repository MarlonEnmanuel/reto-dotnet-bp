using AccountsApi.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using MovementsApi.Application.Interfaces;

namespace AccountsApi.Infrastructure.Controllers
{
    public static class MovementsController
    {
        public static void MapMovementRoutes(this IEndpointRouteBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("/movements").WithOpenApi();

            group.MapGet("", async ([FromServices] IMovementsService service) =>
            {
                return Results.Ok(await service.GetMovements());
            });

            group.MapGet("{id:int}", async (int id, [FromServices] IMovementsService service) =>
            {
                return Results.Ok(await service.GetMovement(id));
            });

            group.MapPost("", async ([FromBody] CreateMovementDto dto, [FromServices] IMovementsService service) =>
            {
                var result = await service.CreateMovement(dto);
                return Results.Created($"/movements/{result.Id}", result);
            });

            group.MapPut("{id:int}", async (int id, [FromBody] UpdateMovementDto dto, [FromServices] IMovementsService service) =>
            {
                var result = await service.UpdateMovement(id, dto);
                return Results.Created($"/movements", result);
            });

            group.MapDelete("{id:int}", async (int id, [FromServices] IMovementsService service) =>
            {
                var result = await service.DeleteMovement(id);
                return Results.Created($"/movements/{result.Id}", result);
            });
        }
    }
}
