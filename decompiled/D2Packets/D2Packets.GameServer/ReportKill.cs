using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x11 - Report Kill
/// <para>A player or his pet has killed something.</para>
/// <para>TEST: Only applies to collateral damage with multi shot attack (like chain lightning etc) ??
/// Seems it applies to Me kills too and UnitType is killer type not victim ??
/// Does it apply only to monster kills or PVP / merc death / etc ??</para>
/// </summary>
public class ReportKill : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public ReportKill(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
	}

	public ReportKill(UnitType type, uint uid)
		: base(Build(type, uid))
	{
		unitType = type;
		this.uid = uid;
	}

	public static byte[] Build(UnitType type, uint uid)
	{
		return new byte[6]
		{
			17,
			(byte)type,
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24)
		};
	}
}
