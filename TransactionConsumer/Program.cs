using EasyNetQ;
using Infra;
using System.Configuration;
using TransactionConsumer;

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddInfra(configuration);
        services.AddTransient<IBus>(provider => RabbitHutch.CreateBus(configuration.GetConnectionString("TransactionPurchaseMessageConnection")));
    })
    .Build();

await host.RunAsync();
