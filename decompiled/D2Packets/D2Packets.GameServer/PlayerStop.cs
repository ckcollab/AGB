using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0D - Player Stop
/// <para>Sent every time another human player completed a walk / cast / etc action.</para>
/// </summary>
public class PlayerStop : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected byte unknown1;

	protected int x;

	protected int y;

	protected byte unknown2;

	protected byte life;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public byte Life => life;

	public byte Unknown1 => unknown1;

	public byte Unknown2 => unknown2;

	public PlayerStop(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		unknown1 = data[6];
		x = BitConverter.ToUInt16(data, 7);
		y = BitConverter.ToUInt16(data, 9);
		unknown2 = data[11];
		life = data[12];
	}
}
