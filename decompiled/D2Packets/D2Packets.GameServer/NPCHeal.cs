using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xAB - NPC Heal
/// <para>An NPC within your range of view has gained some life.</para>
/// <para>TEST: Applies to heal spells too or only regeneration?</para>
/// </summary>
public class NPCHeal : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected byte life;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public byte Life => life;

	public NPCHeal(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		life = data[6];
	}
}
