using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserManagementApp.Application.ViewModels;
using UserManagementApp.Domain.Exceptions;

namespace UserManagementApp.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DataInvalidException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status404NotFound);
            }
            catch (ConflictException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status409Conflict);
            }
            catch (ObjectAlreadyExistedException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status409Conflict);
            }
            catch (InvalidInformationLoginException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status401Unauthorized);
            }
            catch (UserInactiveException ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleExceptionAsync<T>(HttpContext context, T exception, string userMessage, int statusCode) where T : Exception
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var err = new GeneralResponse()
            {
                Success = false,
                Message = userMessage
            };

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(err);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

}