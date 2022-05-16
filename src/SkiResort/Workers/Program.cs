using Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<QueueTimeCountingService>();
        services.AddHostedService<CardReadingReceivingService>();
    })
    .Build();

await host.RunAsync();
