using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models.Message;

public class Message
{
    [JsonProperty(PropertyName ="id")]
    public string Id { get; set; }
    public User User { get; set; }
    public string Content { get; set; } = string.Empty;
    public Channel? Channel { get; set; }

    public Guild? Guild { get; set; }
    public DateTimeOffset Time { get; set; }
    public DateTimeOffset? EditedTime { get; set; }
    public List<Attachment> Attachments { get; set; } = new ();
}