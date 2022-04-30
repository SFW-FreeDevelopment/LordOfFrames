using LordOfFrames.Api.Repositories;

namespace LordOfFrames.Api.Middleware;

internal class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private const string ApiKey = "XApiKey";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApiKeyRepository apiKeyRepository)
    {
        if (!string.Equals(context.Request.Method, "get", StringComparison.OrdinalIgnoreCase))
        {
            if (context.Request.Headers.TryGetValue(ApiKey, out
                    var extractedApiKey))
            {
                var validated = await apiKeyRepository.ValidateKey(extractedApiKey);
                if (!validated)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
        
        await _next(context);
    }
}