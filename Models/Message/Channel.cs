using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models.Message;

public class Channel
{
    [JsonProperty(PropertyName ="id")]
    public ulong Id { get; set; }
    public string Name { get; set; }
}