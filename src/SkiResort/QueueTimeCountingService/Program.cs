using QueueTimeCounting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<QueueTimeCountingService>();
    })
    .Build();

await host.RunAsync();
