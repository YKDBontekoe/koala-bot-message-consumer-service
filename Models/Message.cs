namespace Koala.MessageConsumerService.Models;

public class Message
{
    public User User { get; set; }
    public string Content { get; set; }
    public Channel Channel { get; set; }
}