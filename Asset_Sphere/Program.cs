using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Asset_Sphere.Data;
using Asset_Sphere.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Asset_SphereContextConnection") ?? throw new InvalidOperationException("Connection string 'Asset_SphereContextConnection' not found.");

builder.Services.AddDbContext<Asset_SphereContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Asset_SphereUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Asset_SphereContext>();

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


//The following code will seed roles to the databasa 
//using(var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "Admin", "Technician", "Asset_team_member" };
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }
//}

//builder.Services.AddAuthentication(options =>
//{
//    options.AddPolicy("Admin", policy. => policy.RequreRole("Admin"));
//});
        app.Run();
