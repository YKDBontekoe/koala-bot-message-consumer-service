using Azure.Messaging.ServiceBus;
using Koala.MessageConsumerService.Models.Message;
using Koala.MessageConsumerService.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Repositories;

public class ServiceBusConsumer : IServiceBusConsumer
{
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _client;
    private ServiceBusProcessor? _processor;
    private readonly IMessageRepository _messageRepository;

    public ServiceBusConsumer(
        IConfiguration configuration,
        ServiceBusClient serviceBusClient, IMessageRepository messageRepository)
    {
        _configuration = configuration;
        _client = serviceBusClient;
        _messageRepository = messageRepository;
    }

    public async Task RegisterOnMessageHandlerAndReceiveMessages()
    {
        _processor = _client.CreateProcessor(_configuration["ServiceBus:QueueName"], new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = true,
            MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(15),
            PrefetchCount = 100,
        });
        
        try
        {
            // add handler to process messages
            _processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task CloseQueueAsync()
    {
        if (_processor != null) await _processor.CloseAsync();
    }

    public async Task DisposeAsync()
    {
        if (_processor != null) await _processor.DisposeAsync();
    }

    // handle received messages
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var message = JsonConvert.DeserializeObject<Message>(body);
        await _messageRepository.AddMessageAsync(message);

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

// handle any errors when receiving messages
    private static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}