using System.Text.Json.Serialization;

namespace Topdev.Twitch.Chat.Client.Models
{
    public class ChatInfo
    {
        [JsonPropertyName("chatter_count")]
        public int ChatterCount { get; set; }

        [JsonPropertyName("chatters")]
        public Chatters Chatters { get; set; }
    }
}