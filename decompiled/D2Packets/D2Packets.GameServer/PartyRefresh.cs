using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x53 - Party Refresh
/// <para>You should receive this packet about once per minute, followed by a 0x47 and a 0x48 packet for each player with in your view.</para>
/// <para>The last DWORD seems to count up slowly and goes back to 00 at around 0xAA and the boolean BYTE should toggle between 0x00 and 0x80 for every packet.</para>
/// </summary>
public class PartyRefresh : GSPacket
{
	protected uint slotNumber;

	protected byte alternator;

	protected uint count;

	/// <summary>
	/// UNKNOWN: Not a party ID, not sure about it being some kind of player ID...
	/// </summary>
	public uint SlotNumber => slotNumber;

	/// <summary>
	/// Alternates between 0x00 and 0x80 every packet.
	/// </summary>
	public byte Alternator => alternator;

	/// <summary>
	/// Increments by 8 every packet untill 0xAA after which it restarts at 0
	/// </summary>
	public uint Count => count;

	public PartyRefresh(byte[] data)
		: base(data)
	{
		slotNumber = BitConverter.ToUInt32(data, 1);
		alternator = data[5];
		count = BitConverter.ToUInt32(data, 4);
	}
}
