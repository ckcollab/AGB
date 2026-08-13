using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0xA9 - End State
/// <para>Notifies a potion / aura's effect is over or the unit no longer is covered by it.</para>
/// </summary>
public class EndState : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected StateType state;

	public StateType State => state;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public EndState(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		state = (StateType)data[6];
	}
}
