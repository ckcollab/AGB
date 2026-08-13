using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x22 - Update Player Item Skill
/// <para>Updates availability and/or quantity of an item provided skill (id/tp...)</para>
/// </summary>
public class UpdatePlayerItemSkill : GSPacket
{
	protected ushort unknown1;

	protected uint playerUID;

	protected SkillType skill;

	protected int quantity;

	protected ushort unknown10;

	public uint PlayerUID => playerUID;

	public SkillType Skill => skill;

	public int Quantity => quantity;

	public ushort Unknown1 => unknown1;

	public ushort Unknown10 => unknown10;

	public UpdatePlayerItemSkill(byte[] data)
		: base(data)
	{
		unknown1 = BitConverter.ToUInt16(data, 1);
		playerUID = BitConverter.ToUInt32(data, 3);
		skill = (SkillType)BitConverter.ToUInt16(data, 7);
		quantity = data[9];
		unknown10 = BitConverter.ToUInt16(data, 10);
	}
}
