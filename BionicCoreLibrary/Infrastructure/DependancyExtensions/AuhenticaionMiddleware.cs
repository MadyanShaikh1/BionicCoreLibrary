using BionicCoreLibrary.Common.Constant;

namespace BionicCoreLibrary.Infrastructure.DependancyExtensions;
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var endPoint = context.GetEndpoint().DisplayName;

        var ensureEndPoint = endPoint.Contains("AuthenticationController.Authenticate");

        // Check if the Request is for Authentication
        if (ensureEndPoint)
        {
            await _next(context);
        }
        // Check if the user is authenticated

        else if (!context.User.Identity.IsAuthenticated)
        {
            // User is not authenticated, return a 401 Unauthorized response
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(Constants.Unauthorize);
            return;
        }
        else
        {
            await _next(context);
        }

    }
}


