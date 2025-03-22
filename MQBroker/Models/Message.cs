using System.Text.Json.Serialization;

namespace MQBroker.Models
{
    public class Message
    {
        [JsonPropertyName("type")]
        public required string Type { get; init; }

        [JsonPropertyName("appId")]
        public required string AppId { get; init; }

        [JsonPropertyName("topic")]
        public required string Topic { get; init; }

        [JsonPropertyName("content")]
        public string? Content { get; init; }

        [JsonConstructor]
        public Message(string type, string appId, string topic, string? content = null)
        {
            Type = type;
            AppId = appId;
            Topic = topic;
            Content = content;
        }

        public Message(string type, string appId, string topic)
            : this(type, appId, topic, null)
        { }
    }
}
