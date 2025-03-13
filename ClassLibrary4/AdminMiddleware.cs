using Microsoft.AspNetCore.Http; // Required for HttpContext and ISession
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives; // ✅ Correct namespace for session extension methods
using System.Threading.Tasks;
using System.Text.Json;
using ClassLibrary5; // ✅ This allows access to the new `SessionExtensions` class

using Microsoft.Extensions.DependencyInjection;




namespace ClassLibrary5
{
    public class AdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // ✅ Corrected constructor to accept RequestDelegate
        public AdminMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Session is not available.");
                return;
            }

            var userRole = session.GetString("UserRole") ?? "student";

            if (context.Request.Path.StartsWithSegments("/admin") && userRole != "admin")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You are not an admin, you can't access this page.");
                return;
            }

            await _next(context); // ✅ Pass control to the next middleware
        }
    }
}