using System;
using ETUtils;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x6D - Ping
/// <para>Should be sent every 5 to 7 seconds to stop from dropping from the game.</para>
/// </summary>
public class Ping : GCPacket
{
	protected uint tickCount;

	public uint TickCount => tickCount;

	/// <summary>
	/// Should be a null value on joining the game server, and after that, a non-static value is used.
	/// <para>Varying DWORD between 0x30 and 0x50 seems to allow connection to be maintained...</para>
	/// </summary>
	public string Unknown5 => ByteConverter.ToHexString(data, 5, 8);

	public Ping(byte[] data)
		: base(data)
	{
		tickCount = BitConverter.ToUInt32(data, 1);
	}

	public Ping(uint tickCount, long unknown5)
		: base(Build(tickCount, unknown5))
	{
		this.tickCount = tickCount;
	}

	public static byte[] Build(uint tickCount, long unknown5)
	{
		return new byte[13]
		{
			109,
			(byte)tickCount,
			(byte)(tickCount >> 8),
			(byte)(tickCount >> 16),
			(byte)(tickCount >> 24),
			(byte)unknown5,
			(byte)(unknown5 >> 8),
			(byte)(unknown5 >> 16),
			(byte)(unknown5 >> 24),
			(byte)(unknown5 >> 32),
			(byte)(unknown5 >> 40),
			(byte)(unknown5 >> 48),
			(byte)(unknown5 >> 56)
		};
	}
}
