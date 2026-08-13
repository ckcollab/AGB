using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x20 - Player Attribute Notification
/// <para>Notifies you of a player's stat.</para>
/// </summary>
public class PlayerAttributeNotification : GSPacket
{
	protected uint uid;

	protected StatBase stat;

	public uint UID => uid;

	public StatBase Stat => stat;

	public PlayerAttributeNotification(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		BaseStat baseStat = BaseStat.Get(data[5]);
		int val = BitConverter.ToInt32(data, 6);
		if (baseStat.ValShift > 0)
		{
			val >>= baseStat.ValShift;
		}
		if (baseStat.Signed)
		{
			stat = new SignedStat(baseStat, val);
		}
		else
		{
			stat = new UnsignedStat(baseStat, (uint)val);
		}
	}
}
