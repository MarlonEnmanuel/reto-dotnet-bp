using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using Shared.Models;

namespace Shared.Middlewares
{
    public class GlobalExceptionMiddleware(RequestDelegate next,
                                           ILogger<GlobalExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                logger.LogInformation("Petición recibida {0}", context.Request.Path);
                await next(context);
            }
            catch (NotFoundException ex)
            {
                logger.LogWarning(ex, $"Recurso no encontrado {0}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                });
            }
            catch(BadRequestException ex)
            {
                logger.LogWarning(ex, "Petición incorrecta {0}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                    ErrorDetails = ex.Details,
                });
            }
            catch (ValidationException ex)
            {
                logger.LogWarning(ex, "Datos incorrectos {0}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Algunos datos son incorrectos",
                    ErrorDetails = ex.Errors.Select(e => e.ErrorMessage).ToArray(),
                });
            }
            catch(InternalException ex)
            {
                logger.LogError(ex, "Error interno {0}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = ex.Message,
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error inesperado {0}", context.Request.Path);
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
