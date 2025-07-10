using Microsoft.EntityFrameworkCore;
using DL;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

builder.Services.AddDbContext<JocampoProgramacionNcapasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Proyecto")));

builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Estado>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
