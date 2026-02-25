using Microsoft.EntityFrameworkCore;
using StoreFlow.Data;

var builder = WebApplication.CreateBuilder(args);

//  Faz o servidor escutar na rede local (LAN)
builder.WebHost.UseUrls("http://0.0.0.0:5000");

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        $"Data Source={Path.Combine(AppContext.BaseDirectory, "storeflow.db")}")
);

var app = builder.Build();

//  Aplica migrations automaticamente (cria banco e tabelas)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
