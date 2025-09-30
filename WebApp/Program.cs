using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using StoreApp.Mail;
using StoreApp.Models;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureSession();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRepositoryRegistrations();
builder.Services.ConfigureServiceRegistrations();
builder.Services.ConfigureRouting();
builder.Services.ConfigureApplicationCookie();
 builder.Services.AddDataProtection();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});


app.ConfigureDefaultAdminUser();
app.ConfigureLocalization();
app.ConfigureAndCheckMigration();
app.Run();
