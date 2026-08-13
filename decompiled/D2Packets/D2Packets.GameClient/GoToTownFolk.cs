using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x59 - Go To Town Folk
/// <para>Notify the server of intention to interact with a town folk.</para>
/// <para>First pack sent when clicking on a town folk, before running / walking toward it before actual interaction.</para>
/// </summary>
public class GoToTownFolk : GCPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected uint x;

	protected uint y;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public uint X => x;

	public uint Y => y;

	public GoToTownFolk(byte[] data)
		: base(data)
	{
		unitType = (UnitType)BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
		x = BitConverter.ToUInt32(data, 9);
		y = BitConverter.ToUInt32(data, 13);
	}

	public GoToTownFolk(UnitType unitType, uint uid, uint x, uint y)
		: base(Build(unitType, uid, x, y))
	{
		this.unitType = unitType;
		this.uid = uid;
		this.x = x;
		this.y = y;
	}

	public static byte[] Build(UnitType unitType, uint uid, uint x, uint y)
	{
		return new byte[17]
		{
			89,
			(byte)unitType,
			0,
			0,
			0,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)x,
			(byte)(x >> 8),
			(byte)(x >> 16),
			(byte)(x >> 24),
			(byte)y,
			(byte)(y >> 8),
			(byte)(y >> 16),
			(byte)(y >> 24)
		};
	}
}
