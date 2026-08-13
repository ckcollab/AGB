using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x7F - Party Member Update
/// <para>Updates a party member's information.</para>
/// </summary>
public class PartyMemberUpdate : GSPacket
{
	protected bool isPlayer;

	protected int lifePercent;

	protected uint uid;

	protected AreaLevel area;

	public bool IsPlayer => isPlayer;

	public int LifePercent => lifePercent;

	public AreaLevel Area => area;

	public uint UID => uid;

	public PartyMemberUpdate(byte[] data)
		: base(data)
	{
		isPlayer = Convert.ToBoolean(data[1]);
		lifePercent = BitConverter.ToUInt16(data, 2);
		uid = BitConverter.ToUInt32(data, 4);
		area = (AreaLevel)BitConverter.ToUInt16(data, 8);
	}
}
