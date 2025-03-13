using ClassLibrary5;
using Microsoft.OpenApi.Models;

namespace Fifth_Attempt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add services to the container
            builder.Services.AddControllers();

            // ✅ Configure Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fifth_Attempt API",
                    Version = "v1",
                    Description = "API for user authentication and management",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your-email@example.com",
                        Url = new Uri("https://yourwebsite.com")
                    }
                });

                // ✅ Ensures unique schema names to prevent conflicts
                c.CustomSchemaIds(type => type.FullName);
            });

            // ✅ Register services
            builder.Services.AddSingleton<UserService>();
            // ✅ Enable CORS if needed
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });


            string password = "mypassword123"; // Replace with actual password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            Console.WriteLine($"Hashed Password: {hashedPassword}");
            var app = builder.Build();

            // ✅ Ensure Swagger is enabled only in Development Mode
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fifth_Attempt API v1");
                    c.RoutePrefix = "swagger"; // ✅ Swagger UI at /swagger
                });
            }

            app.UseHttpsRedirection();  // ✅ Ensure HTTPS redirection
            app.UseAuthorization();
            app.UseCors("AllowAll"); // ✅ Enable CORS

            app.MapControllers();

            app.Run();
        }
    }
}
