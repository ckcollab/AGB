using System;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x48 - Relator 2
/// <para>This is sent in relation to another packet, usually to provide the related unit's UID.</para>
/// <para>It's (almost?) always seen along with it's twin, <see cref="T:D2Packets.GameServer.Relator1" />.</para>
/// </summary>
public class Relator2 : GSPacket
{
	protected ushort param1;

	protected uint uid;

	protected uint param2;

	public uint UID => uid;

	public ushort Param1 => param1;

	public uint Param2 => param2;

	public Relator2(byte[] data)
		: base(data)
	{
		param1 = BitConverter.ToUInt16(data, 1);
		uid = BitConverter.ToUInt32(data, 3);
		param2 = BitConverter.ToUInt32(data, 7);
	}

	public Relator2(uint uid, ushort param1, uint param2)
		: base(Build(uid, param1, param2))
	{
		this.param1 = param1;
		this.uid = uid;
		this.param2 = param2;
	}

	public static byte[] Build(uint uid, ushort param1, uint param2)
	{
		return new byte[11]
		{
			72,
			(byte)param1,
			(byte)(param1 >> 8),
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)param2,
			(byte)(param2 >> 8),
			(byte)(param2 >> 16),
			(byte)(param2 >> 24)
		};
	}
}
