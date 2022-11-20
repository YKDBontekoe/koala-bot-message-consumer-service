using Koala.MessageConsumerService.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Koala.MessageConsumerService;

public class MessageConsumerWorker : IHostedService, IDisposable
{
    private readonly IServiceBusConsumer _serviceBusConsumer;

    public MessageConsumerWorker(IServiceBusConsumer serviceBusConsumer)
    {
        _serviceBusConsumer = serviceBusConsumer;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _serviceBusConsumer.RegisterOnMessageHandlerAndReceiveMessages();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusConsumer.DisposeAsync()!;
        await _serviceBusConsumer.CloseQueueAsync()!;
    }

    public void Dispose()
    {
        _serviceBusConsumer.DisposeAsync();
    }
}