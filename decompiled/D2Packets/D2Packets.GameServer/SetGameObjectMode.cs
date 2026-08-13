using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0E - Set Game Object Mode
/// <para>Updates the state of a game object (e.g. mark chest as open.)</para>
/// </summary>
public class SetGameObjectMode : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected byte unknown6;

	protected bool canChangeBack;

	protected GameObjectMode mode;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public GameObjectMode Mode => mode;

	public bool CanChangeBack => canChangeBack;

	public byte Unknown6 => unknown6;

	public SetGameObjectMode(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		unknown6 = data[6];
		canChangeBack = BitConverter.ToBoolean(data, 7);
		mode = (GameObjectMode)BitConverter.ToUInt32(data, 8);
	}
}
