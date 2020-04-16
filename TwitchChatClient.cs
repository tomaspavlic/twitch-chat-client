using System;
using System.Threading;
using System.Threading.Tasks;
using Topdev.Twitch.Chat.Helpers;
using Topdev.Twitch.Chat.MessageClients;
using Topdev.Twitch.Chat.Models;

namespace Topdev.Twitch.Chat
{
    public class TwitchChatClient
    {
        public event EventHandler<Message> MessageReceived;

        public event EventHandler ConnectionClose;

        private IMessageClient _twitchMessageClient;
        
        private MessageParser _parser = new MessageParser();

        public TwitchChatClient()
        {
            
        }

        /// <summary>
        /// Triggered when the connection is closed from any reason.
        /// </summary>
        /// <param name="sender">IMessageClient</param>
        /// <param name="e">null</param>
        private void OnConnectionClosed(object sender, EventArgs e)
        {
            ConnectionClose?.Invoke(this, e);
        }

        /// <summary>
        /// Triggered when raw message received.
        /// </summary>
        /// <param name="sender">IMessageClient</param>
        /// <param name="e">Raw message</param>
        private void OnRawMessageReceived(object sender, string e)
        {
            // About once every five minutes, the server sends a PING.
            // To ensure that your connection to the server is not prematurely terminated, reply with PONG
            if (e.StartsWith("PING"))
                SendPongResponseAsync().Wait();

            if (_parser.TryParsePrivateMessage(e, out var message))
            {
                MessageReceived?.Invoke(this, message);
            }
        }

        /// <summary>
        /// Opens a connection to the server and start receiving messages.
        /// </summary>
        /// <param name="oauth">Your password should be an OAuth token authorized through our API with the chat:read scope (to read messages) and the  chat:edit scope (to send messages)</param>
        /// <param name="nick">Your nickname must be your Twitch username (login name) in lowercase</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ConnectAsync(string oauth, string nick, CancellationToken cancellationToken)
        {
            if (!oauth.StartsWith("oauth:"))
            {
                throw new ArgumentException("OAuth parameter must in format 'oauth:xxxx'");
            }

            _twitchMessageClient = new WebSocketMessageClient();
            _twitchMessageClient.MessageReceived += OnRawMessageReceived;
            _twitchMessageClient.ConnectionClosed += OnConnectionClosed;

            return _twitchMessageClient.ConnectAsync(oauth, nick.ToLower(), cancellationToken);
        }

        /// <summary>
        /// Sends a private message to a channel.
        /// </summary>
        /// <param name="channel">A channel name.</param>
        /// <param name="message">Message to be send. Limited to 512 bytes.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SendMessageAsync(string channel, string message, CancellationToken cancellationToken)
        {
            return _twitchMessageClient.SendMessageAsync($"PRIVMSG #{channel} :{message}", cancellationToken);
        }

        /// <summary>
        /// Joins to given twitch channel. Connection must be established first.
        /// </summary>
        /// <param name="channel">A channel name</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JoinChannelAsync(string channel, CancellationToken cancellationToken)
        {
            return _twitchMessageClient.SendMessageAsync($"JOIN #{channel.ToLower()}", cancellationToken);
        }

        /// <summary>
        /// Departs from a channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task LeaveChannelAsync(string channel, CancellationToken cancellationToken)
        {
            return _twitchMessageClient.SendMessageAsync($"PART #{channel.ToLower()}", cancellationToken);
        }

        /// <summary>
        /// About once every five minutes, the server will send a PING :tmi.twitch.tv. 
        /// To ensure that your connection to the server is not prematurely terminated, reply with PONG :tmi.twitch.tv.
        /// </summary>
        /// <returns></returns>
        private Task SendPongResponseAsync()
        {
            return _twitchMessageClient.SendMessageAsync("PONG :tmi.twitch.tv", CancellationToken.None);
        }
    }
}
