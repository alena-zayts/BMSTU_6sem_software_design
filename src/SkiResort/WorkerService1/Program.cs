using CardReadingReciever;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CardReadingRecieverService>();
    })
    .Build();

await host.RunAsync();
