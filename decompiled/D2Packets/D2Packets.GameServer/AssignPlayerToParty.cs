using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x8D - Assign Player To Party
/// <para>Notifies you that a player is now in a party.</para>
/// </summary>
public class AssignPlayerToParty : GSPacket
{
	protected uint uid;

	protected short partyNumber;

	public uint UID => uid;

	public short PartyNumber => partyNumber;

	public AssignPlayerToParty(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		partyNumber = BitConverter.ToInt16(data, 5);
	}

	public AssignPlayerToParty(uint uid, short partyNumber)
		: base(Build(uid, partyNumber))
	{
		this.uid = uid;
		this.partyNumber = partyNumber;
	}

	public static byte[] Build(uint uid, short partyNumber)
	{
		return new byte[7]
		{
			141,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)partyNumber,
			(byte)(partyNumber >> 8)
		};
	}
}
