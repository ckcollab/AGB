using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x8B - Player Party Relationship
/// <para>Update the party specific relationship flags (in a party, invites...)</para>
/// <para>This is always relative to you unlike regular flags (0x8C) which you get for all players.</para>
/// </summary>
public class PlayerPartyRelationship : GSPacket
{
	protected uint uid;

	protected PartyRelationshipType relationship;

	public uint UID => uid;

	public PartyRelationshipType Relationship => relationship;

	public PlayerPartyRelationship(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		relationship = (PartyRelationshipType)data[5];
	}
}
