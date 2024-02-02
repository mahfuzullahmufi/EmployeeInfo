using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.Extentions;
using EmployeeInfo.Service.Extentions;
using EmployeeInfo.Web.Extentions;
using EmployeeInfo.Web.Setup;
using LinqToDB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.RegisterRepositories(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterFactories();
builder.Services.MapEntities();

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
