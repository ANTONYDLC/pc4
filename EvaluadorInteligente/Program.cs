var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del manejo de errores
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Muestra errores detallados en desarrollo
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Página amigable en producción
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.Run();
