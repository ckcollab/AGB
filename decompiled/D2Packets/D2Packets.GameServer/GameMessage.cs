using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x26 - Game Chat
/// <para>Game message or whisper coming from in game player or player / shrine overhead message.</para>
/// <para>If a shrine displays a overhead message, the message will be a 4 character number specifying the message.</para>
/// </summary>
public class GameMessage : GSPacket
{
	public static readonly int NULL_UInt32 = 0;

	public static readonly int NULL_Int32 = -1;

	protected GameMessageType messageType;

	protected string message;

	protected string playerName = null;

	protected UnitType unitType = UnitType.NotApplicable;

	protected uint uid = 0u;

	protected int random = -1;

	public GameMessageType MessageType => messageType;

	public string Message => message;

	/// <summary>
	/// Only set for game messages.
	/// </summary>
	public string PlayerName => playerName;

	/// <summary>
	/// Only set for overhead messages.
	/// </summary>
	public UnitType UnitType => unitType;

	/// <summary>
	/// Only set for overhead messages.
	/// </summary>
	public uint UID => uid;

	/// <summary>
	/// Only set for overhead messages.
	/// </summary>
	public int Random => random;

	public string Unknown3 => (messageType == GameMessageType.OverheadMessage) ? null : ByteConverter.ToHexString(data, 3, 7);

	public GameMessage(byte[] data)
		: base(data)
	{
		messageType = (GameMessageType)BitConverter.ToInt16(data, 1);
		if (messageType == GameMessageType.OverheadMessage)
		{
			unitType = (UnitType)data[3];
			uid = BitConverter.ToUInt32(data, 4);
			random = BitConverter.ToUInt16(data, 8);
			message = ByteConverter.GetNullString(data, 11);
		}
		else
		{
			playerName = ByteConverter.GetNullString(data, 10);
			message = ByteConverter.GetNullString(data, 11 + playerName.Length);
		}
	}

	/// <summary>
	/// Builds a game message or game whisper packet.
	/// </summary>
	public GameMessage(GameMessageType type, byte charFlags, string charName, string message)
		: base(Build(type, charFlags, charName, message))
	{
		messageType = type;
		playerName = charName;
		this.message = message;
	}

	/// <summary>
	/// Builds an overhead message packet.
	/// </summary>
	public GameMessage(UnitType type, uint uid, ushort random, string message)
		: base(Build(type, uid, random, message))
	{
		messageType = GameMessageType.OverheadMessage;
		this.uid = uid;
		this.random = random;
		this.message = message;
	}

	public static byte[] Build(GameMessageType type, byte charFlags, string charName, string message)
	{
		if (charName == null || charName.Length == 0)
		{
			throw new ArgumentException("charName");
		}
		if (message == null || message.Length == 0)
		{
			throw new ArgumentException("message");
		}
		byte[] bytes = new byte[12 + charName.Length + message.Length];
		bytes[0] = 38;
		bytes[1] = (byte)type;
		bytes[3] = 2;
		bytes[9] = charFlags;
		int i;
		for (i = 0; i < charName.Length; i++)
		{
			bytes[10 + i] = (byte)charName[i];
		}
		i = 0;
		int offset = 11 + charName.Length;
		for (; i < message.Length; i++)
		{
			bytes[offset + i] = (byte)message[i];
		}
		return bytes;
	}

	public static byte[] Build(UnitType type, uint uid, ushort random, string message)
	{
		if (message == null || message.Length == 0)
		{
			throw new ArgumentException("message");
		}
		byte[] bytes = new byte[12 + message.Length];
		bytes[0] = 38;
		bytes[1] = 5;
		bytes[3] = (byte)type;
		bytes[4] = (byte)uid;
		bytes[5] = (byte)(uid >> 8);
		bytes[6] = (byte)(uid >> 16);
		bytes[7] = (byte)(uid >> 24);
		bytes[8] = (byte)random;
		bytes[9] = (byte)(random >> 8);
		for (int i = 0; i < message.Length; i++)
		{
			bytes[11 + i] = (byte)message[i];
		}
		return bytes;
	}
}
