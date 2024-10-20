using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Exceptions;

namespace WebAPI.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(GetJsonString(ex));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(GetJsonString(ex));
        }
    }
    
    private String GetJsonString(Exception ex)
    {
        return JsonSerializer.Serialize(new { error = ex.Message });
    }
}