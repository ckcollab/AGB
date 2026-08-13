using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x7A - Summon Action
/// <para>A player (un)summons a pet (includes merc.)</para>
/// <para>Sent even if the player is not on the same area / act as you, or in your party.</para>
/// <para>If action is UnsummonedOrLostSight, all fields other than the monster UID will be null.</para>
/// </summary>
public class SummonAction : GSPacket
{
	protected SummonActionType actionType;

	protected byte skillTree;

	protected int petType;

	protected uint playerUID;

	protected uint petUID;

	public SummonActionType ActionType => actionType;

	public byte SkillTree => skillTree;

	public int PetType => petType;

	public uint PlayerUID => playerUID;

	public uint PetUID => petUID;

	public SummonAction(byte[] data)
		: base(data)
	{
		actionType = (SummonActionType)data[1];
		skillTree = data[2];
		petType = BitConverter.ToUInt16(data, 3);
		playerUID = BitConverter.ToUInt32(data, 5);
		petUID = BitConverter.ToUInt32(data, 9);
	}
}
