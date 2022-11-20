using Koala.MessageConsumerService.Models.Message;

namespace Koala.MessageConsumerService.Repositories.Interfaces;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
}