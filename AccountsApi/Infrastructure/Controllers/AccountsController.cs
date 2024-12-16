using AccountsApi.Application.Dtos;
using AccountsApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.Infrastructure.Controllers
{
    public static class AccountsController
    {
        public static void MapAccountRoutes(this IEndpointRouteBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("/accounts").WithOpenApi();

            group.MapGet("", async ([FromServices] IAccountsService service) =>
            {
                return Results.Ok(await service.GetAccounts());
            });

            group.MapGet("{number}", async (string number, [FromServices] IAccountsService service) =>
            {
                return Results.Ok(await service.GetAccount(number));
            });

            group.MapPost("", async ([FromBody] CreateAccountDto dto, [FromServices] IAccountsService service) =>
            {
                var result = await service.CreateAccount(dto);
                return Results.Created($"/accounts/{result.Number}", result);
            });

            group.MapPut("{number}", async (string number, [FromBody] UpdateAccountDto dto, [FromServices] IAccountsService service) =>
            {
                var result = await service.UpdateAccount(number, dto);
                return Results.Accepted($"/accounts/{result.Number}", result);
            });

            group.MapDelete("{number}", async (string number, [FromServices] IAccountsService service) =>
            {
                await service.DeleteAccount(number);
                return Results.NoContent();
            });
        }
    }
}
