
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProductManagement.Infrastructure.IoC;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        var connectionString =
       configuration.GetConnectionString("DefaultConnection");

        services.AddInfrastructure(configuration);
        services.AddRabbitMqWorker(configuration);
    })
    .Build();

await host.RunAsync();