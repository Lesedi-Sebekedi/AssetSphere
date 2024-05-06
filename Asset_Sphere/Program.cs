using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Asset_Sphere.Data;
using Asset_Sphere.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Asset_SphereContextConnection") ?? throw new InvalidOperationException("Connection string 'Asset_SphereContextConnection' not found.");

builder.Services.AddDbContext<Asset_SphereContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Asset_SphereUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Asset_SphereContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

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
app.MapRazorPages();

app.Run();
