using Koala.MessageConsumerService.Repositories;
using Koala.MessageConsumerService.Repositories.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Koala.MessageConsumerService;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddAzureClients(builder =>
                {
                    builder.AddServiceBusClient(hostContext.Configuration["ServiceBus:ConnectionString"]);
                });
                
                services.AddScoped<IMessageRepository, MessageRepository>();
                services.AddScoped<IServiceBusConsumer, ServiceBusConsumer>();
                
                services.AddHostedService<MessageConsumerWorker>();
            })
            .UseConsoleLifetime()
            .Build();

        await host.RunAsync();
    }
}