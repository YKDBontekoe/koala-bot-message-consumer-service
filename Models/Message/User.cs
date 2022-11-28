using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models.Message;

public class User
{
    [JsonProperty(PropertyName ="id")]
    public ulong Id { get; set; }
    public string Username { get; set; }
}