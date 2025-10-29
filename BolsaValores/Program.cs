using BolsaValores.Data;
using BolsaValores.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------
// 1. Agregar los servicios personalizados
// --------------------------------------------
builder.Services.AddSingleton(new MarketDataService(SeedData.GetStocks())); // Emisoras base
builder.Services.AddSingleton<PortfolioService>(); // Portafolios de usuarios
builder.Services.AddSingleton<MatchingEngine>();   // Motor de emparejamiento de �rdenes
builder.Services.AddSingleton<ClockService>();     // Control horario del mercado

// --------------------------------------------
// 2. Servicios MVC est�ndar
// --------------------------------------------
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --------------------------------------------
// 3. Configurar pipeline HTTP
// --------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// --------------------------------------------
// 4. Mapear rutas
// --------------------------------------------

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ruta espec�fica para el simulador de la Bolsa Mexicana
app.MapControllerRoute(
    name: "bmv",
    pattern: "BolsaMexicana/Simulador",
    defaults: new { controller = "BolsaMexicana", action = "Simulador" });

// --------------------------------------------
// 5. Ejecutar aplicaci�n
// --------------------------------------------
app.Run();
