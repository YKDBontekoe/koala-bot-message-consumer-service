using Koala.MessageConsumerService.Models.Message;
using Koala.MessageConsumerService.Options;
using Koala.MessageConsumerService.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Koala.MessageConsumerService.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly CosmosClient _database;
    private readonly CosmosDbOptions _options;

    public MessageRepository(IOptions<CosmosDbOptions> cosmosDbOptions)
    {
        _options = cosmosDbOptions != null ? cosmosDbOptions.Value : throw new ArgumentNullException(nameof(cosmosDbOptions));
        _database = new CosmosClient(_options.ConnectionString);
    }

    public async Task AddMessageAsync(Message message)
    {
        var container = _database.GetContainer(_options.DatabaseName, _options.ContainerName);
        await container.UpsertItemAsync(message);
    }
}