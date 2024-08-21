namespace HereAndNow.Authentication;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // public async Task InvokeAsync(HttpContent context)
    // {
    //     if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
    //     {
    //         context.Response.StatusCode = 401;
    //         await context.Response.WriteAsync("API key missing");
    //         return;
    //     }
    // }
}
