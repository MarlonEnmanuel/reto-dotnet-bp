using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Shared.Models;

namespace Shared.Middlewares
{
    public class GlobalExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                });
            }
            catch(BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                    ErrorDetails = ex.Details,
                });
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Algunos datos son incorrectos",
                    ErrorDetails = ex.Errors.Select(e => e.ErrorMessage).ToArray(),
                });
            }
            catch(InternalException ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Ocurrió un error inesperado",
                    ErrorDetails = [ex.Message],
                    Exception = ex.ToString(),
                });
            }
        }
    }
}
