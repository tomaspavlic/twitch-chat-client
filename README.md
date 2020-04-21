# Twitch Chat Client

Twitch offers an IRC interface to our chat functionality. This allows you to, for instance:

Develop one or more bots for your channel.
Connect to a channel’s chat with an IRC client instead of using the Web interface. Some developers prefer IRC because it fits their existing workflow - for example, when they already have several  non-Twitch IRC channels. Other developers prefer IRC because it is lighter weight (for machines that are less powerful than the web interface).

> The library currently support WebSocket only. The IRC will be implemented as well.

## Usage

### Connect
```csharp
var client = new TwitchChatClient();
```

### Subscribe for events
```csharp
client.ConnectionClose += OnConnectionClosed;
```

### Connect and receive messages
```csharp
await client.ConnectAsync("oauth:bzccixdfsdf5uwz2qrlkad390jz1cp", "username", cts.Token);
var channel = await client.JoinChannelAsync("onscreen", cts.Token);
channel.MessageReceived += OnMessageReceived;
```

### Get channel chat information
Get information like number of viewers, list of viewers, admins, staff, global mods, etc..
```csharp
var info = await channel.GetChatInformationAsync();
```

## How to generate OAuth token
To authenticate, your password (`PASS`) should be an OAuth token authorized through our API with the `chat:read` scope (to read messages) and the `chat:edit` scope (to send messages).

The token must have the prefix `oauth:`. For example, if your token is `abcd`, you send `oauth:abcd`.
To quickly get a token for your account, use this [Twitch Chat OAuth Password Generator](https://twitchapps.com/tmi/).