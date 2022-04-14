using CardReadingsReceivingWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CardReadingReceivingService>();
    })
    .Build();

await host.RunAsync();
