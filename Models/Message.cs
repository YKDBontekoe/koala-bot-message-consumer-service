﻿using Newtonsoft.Json;

namespace Koala.MessageConsumerService.Models;

public class Message
{
    [JsonProperty(PropertyName ="id")]
    public string Id { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public Channel Channel { get; set; }

    public Guild? Guild { get; set; }
    public DateTimeOffset Time { get; set; }
    public DateTimeOffset? EditedTime { get; set; }
}