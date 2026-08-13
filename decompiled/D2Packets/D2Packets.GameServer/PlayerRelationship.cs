using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x8C - Player Relationship
/// <para>Update the relationship flags (squelch, etc) between two players.</para>
/// <para>This packet will cover all players in the game, including your own character.</para>
/// <para>If the relationship is changed by more than one player a packet will be sent for each player. 
/// E.g. turning hostile on someone will generally make him hostile to you automatically...</para>
/// </summary>
public class PlayerRelationship : GSPacket
{
	protected uint subjectUID;

	protected uint objectUID;

	protected PlayerRelationshipType relations;

	public uint SubjectUID => subjectUID;

	public uint ObjectUID => objectUID;

	public PlayerRelationshipType Relations => relations;

	public PlayerRelationship(byte[] data)
		: base(data)
	{
		subjectUID = BitConverter.ToUInt32(data, 1);
		objectUID = BitConverter.ToUInt32(data, 5);
		relations = (PlayerRelationshipType)BitConverter.ToUInt16(data, 9);
	}
}
