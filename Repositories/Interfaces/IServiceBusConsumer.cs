namespace Koala.MessageConsumerService.Repositories.Interfaces;

public interface IServiceBusConsumer
{
    Task RegisterOnMessageHandlerAndReceiveMessages();
    Task? CloseQueueAsync();
    Task? DisposeAsync();
}