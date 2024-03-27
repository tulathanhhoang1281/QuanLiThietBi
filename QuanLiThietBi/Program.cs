global using QuanLiThietBi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Infrastructure.UnitOfWork;
using QuanLiThietBi.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<qlthietbiContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectionString);
});
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
