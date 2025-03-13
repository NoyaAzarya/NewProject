using StudyMacth.Components;
using ClassLibrary5;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// ✅ Enable Session and Required Services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// ✅ Add Blazor Components
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// ✅ Configure HTTP Client
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5069/")
});

// ✅ Register Application Services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ProtectedLocalStorage>();

// ✅ Add Controllers
builder.Services.AddControllers();

var app = builder.Build();


// ✅ Enable Middleware & Session
app.UseSession();
app.UseMiddleware<AdminMiddleware>();

// ✅ Configure Exception Handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseAntiforgery();

// ✅ Enable API Controllers
app.MapControllers();

// ✅ Map Blazor Components
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
