using FacturacionWeb.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a la base de datos y registrar el DbContext.
var connectionString = builder.Configuration.GetConnectionString("FacturacionConnection");

//  Verificar que la cadena de conexión no sea nula.
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'FacturacionConnection' no se encontró o está vacía en appsettings.json.");
}

builder.Services.AddDbContext<FacturacionContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Agregar los servicios de MVC al contenedor de dependencias.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Configurar el pipeline de peticiones HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // Se comenta para evitar el error en un entorno de desarrollo sin HTTPS configurado.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. Definir la ruta por defecto para los controladores MVC.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();