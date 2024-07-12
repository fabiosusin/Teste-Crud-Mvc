using DAO.DBConnection;
using DAO.DependencyInjection;
using Microsoft.Extensions.Options;
using Teste_Crud_Mvc.Services.DependencyInjection;


var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var appSettings = new DatabaseSettings();
new ConfigureFromConfigurationOptions<DatabaseSettings>(config.GetSection("DatabaseSettings")).Configure(appSettings);
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)));
builder.Services.AddScoped(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddScoped<IDatabaseSettings, DatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddControllersWithViews();

CreateDaoDependencyInjection.RegisterDependencyInjection(builder.Services);
CreateServicesDependencyInjection.RegisterDependencyInjection(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Customer/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=List}/{id?}");

app.Run();
