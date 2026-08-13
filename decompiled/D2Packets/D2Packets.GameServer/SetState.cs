using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA8 - Set State
/// <para>Notifies to start applying potion / aura / cast delay state effect.</para>
/// <para>The server will send you this packet followed by a 0x47 and 0x48 for every player covered by the aura/spell.</para>
/// </summary>
public class SetState : GSPacket
{
	public static readonly int NULL_Int32 = -1;

	protected UnitType unitType;

	protected uint uid;

	protected UnitState state;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public UnitState State => state;

	public SetState(byte[] data)
		: base(data)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		state = new UnitState(data[7]);
		BitReader br = new BitReader(data, 8);
		while (true)
		{
			bool flag = true;
			int statID = br.ReadInt32(9);
			if (statID == 511)
			{
				break;
			}
			BaseStat stat = BaseStat.Get(statID);
			int val = br.ReadInt32(stat.SendBits);
			if (stat.SendParamBits > 0)
			{
				int param = br.ReadInt32(stat.SendParamBits);
				if (stat.Signed)
				{
					state.Stats.Add(new SignedStatParam(stat, val, param));
				}
				else
				{
					state.Stats.Add(new UnsignedStatParam(stat, (uint)val, (uint)param));
				}
			}
			else if (stat.Signed)
			{
				state.Stats.Add(new SignedStat(stat, val));
			}
			else
			{
				state.Stats.Add(new UnsignedStat(stat, (uint)val));
			}
		}
	}
}
