using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuitoVibesMvc.Data;
using QuitoVibesMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Servicios y Base de Datos (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=quitovibes.db"));

// 2. Servicio de Seguridad (MD5 / SHA-256)
builder.Services.AddScoped<ISecurityService, SecurityService>();

// 3. Configuración de Autenticación basada en Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ReturnUrlParameter = "ReturnUrl";
        options.ExpireTimeSpan = TimeSpan.FromHours(4);
        options.SlidingExpiration = true;
    });

// 4. Controladores y Vistas MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 5. Inicialización de Datos (Seed Database)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var securityService = services.GetRequiredService<ISecurityService>();
        DbInitializer.Initialize(context, securityService);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar la base de datos.");
    }
}

// 6. Pipeline de Solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HttpsRedirection desactivado para facilitar la ejecución en HTTP local sin certificados
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware de Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
