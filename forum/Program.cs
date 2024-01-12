using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using forum.Areas.Identity.Data;
using forum.Data;
using forum.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("forumDBContextConnection") ?? throw new InvalidOperationException("Connection string 'forumDBContextConnection' not found.");

builder.Services.AddDbContext<ForumDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ForumDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Update password policy
    options.Password.RequiredLength = 2;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
});
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application starting...");
logger.LogInformation($"args.Length: {args.Length}, args[0]: {(args.Length > 0 ? args[0] : "N/A")}");

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    logger.LogInformation("In seed ...");

    await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}
logger.LogInformation("After  seed...");

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