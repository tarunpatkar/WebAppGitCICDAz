using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using WebAppGitCICDAz.Data;

var builder = WebApplication.CreateBuilder(args);
var connectstring = builder.Configuration.GetConnectionString("AzureSqlConnection");

//var keyVaultName = new Uri(builder.Configuration["KeyVaultUrl"]);
//var secretClient = new SecretClient(keyVaultName, new DefaultAzureCredential());
//var cs = secretClient.GetSecret("azsql").Value.Value;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectstring));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
