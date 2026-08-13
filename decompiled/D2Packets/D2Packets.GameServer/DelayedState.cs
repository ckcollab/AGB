using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA7 - Delayed State
/// <para>A player casts/selects a long lasting skill like a frozen orb/blizzard etc.</para>
/// <para>Only seems related to a few auras, long lasting spells and when a monster gets frozen / stunned.</para>
/// </summary>
public class DelayedState : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected BaseState state;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public BaseState State => state;

	public DelayedState(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		state = BaseState.Get(data[6]);
	}
}
