global using QuanLiThietBi.Domain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Infrastructure.UnitOfWork;
using QuanLiThietBi.Models;
using System.Configuration;
using QuanLiThietBi.Infrastructure.Repositories;
using QuanLiThietBi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QuanLiThietBi.Models.qlthietbiContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);
builder.Services.AddDbContext<QuanLiThietBi.Infrastructure.qlthietbiContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = new PathString("/LoginReg/Index");
    options.LogoutPath = new PathString("/LoginReg/Logout");
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.SlidingExpiration = true;
}); 

builder.Services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "qlthietbiSession";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(24, 0, 0);    // Thời gian tồn tại của Session
    cfg.Cookie.HttpOnly = true;
    cfg.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IRepository<TblMaintenance>, MaintenanceRepository>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IRepository<TblProduct>,ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<QuanLiThietBi.Models.qlthietbiContext>();
    context.Database.Migrate();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<QuanLiThietBi.Infrastructure.qlthietbiContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginReg}/{action=Index}/");

app.Run();
