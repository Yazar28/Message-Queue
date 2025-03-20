using System.Text.Json.Serialization;

namespace MQBroker.Models
{
    public class Message
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("appId")]
        public string AppId { get; }

        [JsonPropertyName("topic")]
        public string Topic { get; }

        [JsonPropertyName("content")]
        public string? Content { get; }

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
