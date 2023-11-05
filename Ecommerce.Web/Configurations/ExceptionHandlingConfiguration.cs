using System.Text.Json;
using System.Text.Json.Serialization;
using Ecommerce.Common.Contract.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Web.Configurations
{
    public class ExceptionHandling
    {
    }
    
    public static class ExceptionHandlingConfiguration
    {
        public static IApplicationBuilder UseExceptionConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature.Error;

                    var logger = context.RequestServices.GetService<ILogger<ExceptionHandling>>();
                    logger.LogError(exception.ToString());

                    var errorsFactory = context.RequestServices.GetRequiredService<IErrorsFactory>();

                    var error = errorsFactory.CreateBaseError(context, exception, StatusCodes.Status400BadRequest);

                    context.Response.StatusCode = error.Status ?? StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(error, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                });
            });

            return app;
        }
    }
}