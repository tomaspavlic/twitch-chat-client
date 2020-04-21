using System.Text.Json.Serialization;

namespace Topdev.Twitch.Chat.Client.Models
{
    public class ChatInfo
    {
        [JsonPropertyName("chatter_count")]
        public int ChatterCount { get; set; }

        [JsonPropertyName("chatters")]
        public Chatters Chatters { get; set; }

        [JsonPropertyName("staff")]
        public string[] Staff { get; set; }

        [JsonPropertyName("admins")]
        public string[] Admins { get; set; }

        [JsonPropertyName("global_mods")]
        public string[] GlobalMods { get; set; }

        [JsonPropertyName("viewers")]
        public string[] Viewers { get; set; }
    }
}