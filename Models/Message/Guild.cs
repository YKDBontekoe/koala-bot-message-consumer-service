using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models.Message;

public class Guild
{
    [JsonProperty(PropertyName ="id")]
    public ulong Id { get; set; }
    public string Name { get; set; }
    public bool IsNsfw { get; set; }
}