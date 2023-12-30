using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using forum.Data;
using forum.Areas.Identity.Data;
using forum.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("forumDBContextConnection") ?? throw new InvalidOperationException("Connection string 'forumDBContextConnection' not found.");

builder.Services.AddDbContext<ForumDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AuthenUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ForumDbContext>(); // Corrected to use ForumDbContext

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