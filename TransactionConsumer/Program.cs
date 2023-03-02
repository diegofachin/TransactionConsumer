using Infra;
using System.Configuration;
using TransactionConsumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //services.AddInfra(services.Configuration);
    })
    .Build();

await host.RunAsync();
