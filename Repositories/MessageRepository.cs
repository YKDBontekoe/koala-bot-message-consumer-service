using Koala.MessageConsumerService.Models.Message;
using Koala.MessageConsumerService.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Koala.MessageConsumerService.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly CosmosClient _database;
    private readonly IConfiguration _configuration;

    public MessageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _database = new CosmosClient(configuration["CosmosDb:ConnectionString"]);
    }

    public async Task AddMessageAsync(Message message)
    {
        var container = _database.GetContainer(_configuration["CosmosDb:DatabaseName"], _configuration["CosmosDb:ContainerName"]);
        await container.CreateItemAsync(
            item: message,
            partitionKey: new PartitionKey(message.Id));
    }
}