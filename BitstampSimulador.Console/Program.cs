using BitstampSimulador.Domain.Entities;
using BitstampSimulador.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<MongoService>();
        services.AddHostedService<ServicoWebSocket>();
    })
    .Build();

await host.RunAsync();