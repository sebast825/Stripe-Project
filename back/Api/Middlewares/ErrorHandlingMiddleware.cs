using Aplication.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                int statusCode = GetStatusCode(ex);
                LogError(ex, statusCode);
                await HandleExceptionAsync(context, ex, statusCode);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
        {


            var (message, errorType) = GetSafeErrorMessage(ex, statusCode);

            ErrorResponseDto response = new ErrorResponseDto
            {
                Success = false,
                Error = errorType,
                Message = message
            };

            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }

        private (string message, string errorType) GetSafeErrorMessage(Exception ex, int statusCode)
        {
            if (!_env.IsDevelopment() && statusCode >= 500)
            {
                return ("An internal server error occurred", "InternalError");
            }

            return (ex.Message, ex.GetType().Name);
        }
        private int GetStatusCode(Exception ex)
        {
            int statusCode = ex switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                InvalidCredentialException => StatusCodes.Status401Unauthorized,

                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            return statusCode;
        }
        private void LogError(Exception ex, int statusCode)
        {
            if (statusCode < 500)
            {
                _logger.LogInformation(ex, "Client error: {Message}", ex.Message);
            }
            else
            {
                _logger.LogError(ex, "Server error: {Message}", ex.Message);
            }
        }
    }
}
