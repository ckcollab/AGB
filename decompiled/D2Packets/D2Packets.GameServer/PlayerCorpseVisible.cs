using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x74 - Player Corpse Visibility
/// <para>Assigns a corpse UID to a slain player or remove a picked up corpse.</para>
/// </summary>
public class PlayerCorpseVisible : GSPacket
{
	protected bool assign;

	protected uint playerUID;

	protected uint corpseUID;

	public bool Assign => assign;

	public uint PlayerUID => playerUID;

	public uint CorpseUID => corpseUID;

	public PlayerCorpseVisible(byte[] data)
		: base(data)
	{
		assign = Convert.ToBoolean(data[1]);
		playerUID = BitConverter.ToUInt32(data, 2);
		corpseUID = BitConverter.ToUInt32(data, 6);
	}

	public PlayerCorpseVisible(bool assign, uint playerUID, uint corpseUID)
		: base(Build(assign, playerUID, corpseUID))
	{
		this.assign = assign;
		this.playerUID = playerUID;
		this.corpseUID = corpseUID;
	}

	public static byte[] Build(bool assign, uint playerUID, uint corpseUID)
	{
		return new byte[10]
		{
			116,
			(byte)(assign ? 1u : 0u),
			(byte)playerUID,
			(byte)(playerUID >> 8),
			(byte)(playerUID >> 16),
			(byte)(playerUID >> 24),
			(byte)corpseUID,
			(byte)(corpseUID >> 8),
			(byte)(corpseUID >> 16),
			(byte)(corpseUID >> 24)
		};
	}
}
