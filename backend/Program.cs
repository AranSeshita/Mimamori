
using System.Reflection;
using Hangfire;
using Hangfire.Dashboard;
using Mimamori.Applications.Services;
using Mimamori.Applications.Services.Abstractions;
using Mimamori.Configs;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
builder.Host.UseOrleans((ctx, siloBuilder) =>
{
    if (ctx.HostingEnvironment.IsDevelopment())
    {
        var azureStorageConnectionString = ctx.Configuration.GetValue<string>("AZURE_STORAGE_CONNECTION_STRING");
        siloBuilder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "Mimamori";
            options.ServiceId = "Mimamori";
        })
        .UseAzureStorageClustering(options => options.ConfigureTableServiceClient(azureStorageConnectionString));
        siloBuilder.UseLocalhostClustering();

        // siloBuilder.AddAzureBlobGrainStorage("helloStore", config => config.ConfigureBlobServiceClient(azureStorageConnectionString));
        siloBuilder.UseDashboard();
    }
}).ConfigureLogging(logging => logging.AddConsole());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add Hangfire services.
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(configuration.GetValue<string>("ConnectionStrings:HangfireConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(options =>
{
    options.ServerName = "Mimamori.Local";
});

builder.Services.Configure<PresenceDatabaseSettings>(builder.Configuration.GetSection("PresenceDatabaseSettings"));
builder.Services.AddTransient<IPresenceService, PresenceService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Map("/dashboard", x => x.UseOrleansDashboard());
}
app.UseAuthorization();
app.UseHangfireDashboard("/hf", new DashboardOptions
{
    Authorization = new[] { new MyAuthorizationFilter() }
});
app.MapControllers();
app.Run();
