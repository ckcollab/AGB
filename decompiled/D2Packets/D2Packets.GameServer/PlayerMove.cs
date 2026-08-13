using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x0F - Player Move
/// <para>Another player is moving to a new location in your range of view.</para>
/// </summary>
public class PlayerMove : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected byte movementType;

	protected int targetX;

	protected int targetY;

	protected byte unknown12;

	protected int currentX;

	protected int currentY;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	/// <summary>
	/// TODO: make enum... 0x23=Run, 0x01=Walk, 0x20=knocked back ?
	/// </summary>
	public byte MovementType => movementType;

	public int TargetX => targetX;

	public int TargetY => targetY;

	public int CurrentX => currentX;

	public int CurrentY => currentY;

	public byte Unknown12 => unknown12;

	public PlayerMove(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		movementType = data[6];
		targetX = BitConverter.ToUInt16(data, 7);
		targetY = BitConverter.ToUInt16(data, 9);
		unknown12 = data[12];
		currentX = BitConverter.ToUInt16(data, 12);
		currentY = BitConverter.ToUInt16(data, 14);
	}
}
