using System;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x78 - Accept Trade
/// <para>Notifies you of who you are trading with after you have accepted a trade request.</para>
/// </summary>
public class AcceptTrade : GSPacket
{
	protected string playerName;

	protected uint playerUID;

	public string PlayerName => playerName;

	public uint PlayerUID => playerUID;

	public AcceptTrade(byte[] data)
		: base(data)
	{
		playerName = ByteConverter.GetNullString(data, 1, 16);
		playerUID = BitConverter.ToUInt32(data, 17);
	}

	public AcceptTrade(string name, uint uid)
		: base(Build(name, uid))
	{
		playerName = name;
		playerUID = uid;
	}

	public static byte[] Build(string name, uint uid)
	{
		if (name == null || name.Length == 0 || name.Length > 16)
		{
			throw new ArgumentException("name");
		}
		byte[] bytes = new byte[21]
		{
			120,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
		for (int i = 0; i < name.Length; i++)
		{
			bytes[1 + i] = (byte)name[i];
		}
		return bytes;
	}
}
