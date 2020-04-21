using System.Text.Json.Serialization;

namespace Topdev.Twitch.Chat.Client.Models
{
    public class Chatters
    {
        [JsonPropertyName("broadcaster")]
        public string[] Brodcaster { get; set; }

        [JsonPropertyName("vips")]
        public string[] Vips { get; set; }

        [JsonPropertyName("moderators")]
        public string[] Moderators { get; set; }
    }
}