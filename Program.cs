using blog.Data;
using blog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetSection("ConnectionString").Value));

builder.Services.AddTransient<IRepository, Repository>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        // test settings
        // TODO update requirements.
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

try
{
    // seed db with admin user.
    
    var scope = app.Services.CreateScope();
    
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    dbContext.Database.EnsureCreated();

    var adminRole = new IdentityRole("Admin");

    if (!dbContext.Roles.Any())
    {
        // create role
        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    if (!dbContext.Users.Any(u => u.UserName == "admin"))
    {
        // create user
        var adminUser = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@test.com"
        };
        userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();