namespace D2Packets.BnetServer;

/// <summary>
/// Chat event type.
/// <para>Used by <see cref="T:D2Packets.BnetServer.ChatEvent" />.</para>
/// </summary>
public enum ChatEventType : uint
{
	/// <summary>
	/// Received for every user when you join a channel.
	/// <para>Also sent when a user requires an update to his statstring.</para>
	/// </summary>
	ChannelUser = 1u,
	/// <summary>
	/// Someone joins the channel you're currently in.
	/// </summary>
	ChannelJoin = 2u,
	/// <summary>
	/// Someone leaves the channel you're currently in.
	/// </summary>
	ChannelLeave = 3u,
	/// <summary>
	/// Received whisper message.
	/// </summary>
	ReceiveWhisper = 4u,
	/// <summary>
	/// Received when someone talks in the channel you're currently in.
	/// </summary>
	ChannelMessage = 5u,
	/// <summary>
	/// Server information broadcast.
	/// </summary>
	ServerBroadcast = 6u,
	/// <summary>
	/// Received when you join a channel (channel's name and flags.)
	/// </summary>
	ChannelInfo = 7u,
	/// <summary>
	/// Update a user's flags.
	/// </summary>
	UserFlags = 9u,
	/// <summary>
	/// Sent whisper message receipt.
	/// </summary>
	WhisperSent = 10u,
	ChannelFull = 13u,
	ChannelDoesNotExist = 14u,
	ChannelRestricted = 15u,
	/// <summary>
	/// Account is the one you are logged on. Used to be the BattleNetAdministrator who sent the message.
	/// </summary>
	Broadcast = 18u,
	Error = 19u,
	Emote = 23u
}
