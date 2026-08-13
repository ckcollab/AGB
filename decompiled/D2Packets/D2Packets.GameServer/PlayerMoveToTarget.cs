using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x10 - Player Move To Target
/// <para>Another human player is moving towards another unit in your range of view.</para>
/// </summary>
public class PlayerMoveToTarget : GSPacket
{
	protected UnitType unitType;

	protected uint uid;

	protected byte movementType;

	protected UnitType targetType;

	protected uint targetUID;

	protected int currentX;

	protected int currentY;

	public UnitType UnitType => unitType;

	public uint UID => uid;

	public byte MovementType => movementType;

	public UnitType TargetType => targetType;

	public uint TargetUID => targetUID;

	public int CurrentX => currentX;

	public int CurrentY => currentY;

	public PlayerMoveToTarget(byte[] data)
		: base(data)
	{
		unitType = (UnitType)data[1];
		uid = BitConverter.ToUInt32(data, 2);
		movementType = data[6];
		targetType = (UnitType)data[7];
		targetUID = BitConverter.ToUInt32(data, 8);
		currentX = BitConverter.ToUInt16(data, 12);
		currentY = BitConverter.ToUInt16(data, 14);
	}
}
