using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x27 - NPC Info
/// <para>If UID is yours, this packet contains your mercenary's info.</para>
/// <para>Otherwise it's info of a town folk when interacting...</para>
/// </summary>
public class NPCInfo : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public string Unknown6 => ByteConverter.ToHexString(data, 6, 34);

	public NPCInfo(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}
}
