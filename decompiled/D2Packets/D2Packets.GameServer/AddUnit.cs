using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xAA - Add Unit
/// <para>Create new Unit or mark old one as valid (generally within 4 screen of player).</para>
/// </summary>
public class AddUnit : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected UnitStates states;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public UnitStates States => states;

	public AddUnit(byte[] data)
		: base(data)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		states = new UnitStates();
		BitReader br = new BitReader(data, 7);
		while (true)
		{
			bool flag = true;
			int stateID = br.ReadInt32(8);
			if (stateID == 255)
			{
				break;
			}
			UnitState state = new UnitState(stateID);
			if (br.ReadBoolean(1))
			{
				while (true)
				{
					flag = true;
					int statID = br.ReadInt32(9);
					if (statID == 511)
					{
						break;
					}
					BaseStat baseStat = BaseStat.Get(statID);
					int val = br.ReadInt32(baseStat.SendBits);
					if (baseStat.SendParamBits > 0)
					{
						int param = br.ReadInt32(baseStat.SendParamBits);
						if (baseStat.Signed)
						{
							state.Stats.Add(new SignedStatParam(baseStat, val, param));
						}
						else
						{
							state.Stats.Add(new UnsignedStatParam(baseStat, (uint)val, (uint)param));
						}
					}
					else if (baseStat.Signed)
					{
						state.Stats.Add(new SignedStat(baseStat, val));
					}
					else
					{
						state.Stats.Add(new UnsignedStat(baseStat, (uint)val));
					}
				}
			}
			states.Add(state);
		}
	}
}
