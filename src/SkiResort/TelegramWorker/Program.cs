using TelegramWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<TelegramService>();
    })
    .Build();

await host.RunAsync();
