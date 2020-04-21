using System;
using Topdev.Twitch.Chat.Models;

namespace Topdev.Twitch.Chat.Client.Models
{
    public interface IChannel
    {
        ChatInfo Info { get; }
        string Name { get; }
        event EventHandler<Message> MessageReceived;
    }
}