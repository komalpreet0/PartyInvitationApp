using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PartyInvitationApp.Data;
using PartyInvitationApp.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configure SQL Server LocalDB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<EmailService>();

var app = builder.Build();

// Apply Migrations Automatically for SQL Server LocalDB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Ensure database is updated
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
