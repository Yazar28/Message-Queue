using System.Text.Json.Serialization;

namespace MQClient.Models
{
    public class Message
    {
        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("appId")]
        public string AppId { get; init; }

        [JsonPropertyName("topic")]
        public string Topic { get; init; }

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
    }
}
