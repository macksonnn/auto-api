//using FluentValidation.Results;
//using System.Net;
//using System.Text.Json;
//using Microsoft.AspNetCore.Builder;
//using FluentValidation;
//using Microsoft.AspNetCore.Http;

//namespace Core.Common.Middlewares;

//public class ValidationMiddleware(RequestDelegate next, FluentValidation.valida)
//{
//    public async Task InvokeAsync(HttpContext context)
//    {
//        try
//        {
//            await next(context);
//        }
//        catch (Exception e)
//        {
//            await HandleExceptionAsync(context, e);
//        }
//    }
//}

//public static class ValidationMiddlewareExtensions
//{
//    public static IApplicationBuilder UseValidationMiddleware(
//        this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<ErrorMiddleware>();
//    }
//}