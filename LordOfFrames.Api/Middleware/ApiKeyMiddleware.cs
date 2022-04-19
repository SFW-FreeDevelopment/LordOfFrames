namespace LordOfFrames.Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private const string ApiKey = "XApiKey";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(ApiKey, out
                var extractedApiKey))
        {
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }
}