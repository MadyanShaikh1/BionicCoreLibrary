using BionicCoreLibrary.Common.Constant;

namespace BionicCoreLibrary.DependancyInjections.DependancyExtensions;
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        // Check if the user is authenticated
        if (!context.User.Identity.IsAuthenticated)
        {
            // User is not authenticated, return a 401 Unauthorized response
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(Constants.Unauthorize);
            return;
        }
        // User is authenticated, continue with the request pipeline
        await _next(context);
    }
}


