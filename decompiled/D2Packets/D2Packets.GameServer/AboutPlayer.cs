using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x75 - About Player
/// <para>Refresh relationship information with a player.</para>
/// </summary>
public class AboutPlayer : GSPacket
{
	protected uint uid;

	protected short partyID;

	protected int level;

	protected PlayerRelationshipType relationship;

	protected bool isInMyParty;

	protected byte unknown12;

	public uint UID => uid;

	public short PartyID => partyID;

	public int Level => level;

	public PlayerRelationshipType Relationship => relationship;

	public bool IsInMyParty => isInMyParty;

	public byte Unknown12 => unknown12;

	public AboutPlayer(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		partyID = BitConverter.ToInt16(data, 5);
		level = BitConverter.ToUInt16(data, 7);
		relationship = (PlayerRelationshipType)BitConverter.ToUInt16(data, 9);
		isInMyParty = BitConverter.ToBoolean(data, 11);
		unknown12 = data[12];
	}
}
