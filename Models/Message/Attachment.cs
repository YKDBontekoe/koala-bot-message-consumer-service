using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models.Message;

public class Attachment
{
    [JsonProperty(PropertyName ="id")]
    public string Id { get; set; }
    public string Url { get; set; }
    public string ProxyUrl { get; set; }
    public string Filename { get; set; }
    public int Size { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
}