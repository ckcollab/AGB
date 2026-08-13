using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x09 - Assign Warp
/// <para>Notifies you that a door to another area has come into your range of view.</para>
/// <para>When getting within one screen of a warp, this this packet is sent again for the warp on the other side.</para>
/// </summary>
public class AssignWarp : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected WarpType id;

	protected int x;

	protected int y;

	public uint UID => uid;

	public WarpType ID => id;

	public int X => x;

	public int Y => y;

	public UnitType UnitType => unitType;

	public AssignWarp(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		id = (WarpType)data[6];
		x = BitConverter.ToUInt16(data, 7);
		y = BitConverter.ToUInt16(data, 9);
	}
}
