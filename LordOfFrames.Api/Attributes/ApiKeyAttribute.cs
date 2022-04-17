using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LordOfFrames.Api.Attributes;

[AttributeUsage(validOn: AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKey = "XApiKey";
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401
            };
            return;
        }
 
        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
 
        var apiKey = appSettings.GetValue<string>(ApiKey);
 
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401
            };
            return;
        }
 
        await next();
    }
}