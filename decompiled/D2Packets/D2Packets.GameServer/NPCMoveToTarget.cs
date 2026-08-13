using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x68 - NPC Move To Target
/// <para>An NPC is moving to an object in range of view.</para>
/// </summary>
public class NPCMoveToTarget : GSPacket
{
	protected uint uid;

	protected byte movementType;

	protected int currentX;

	protected int currentY;

	protected UnitType targetType;

	protected uint targetUID;

	public uint UID => uid;

	public byte MovementType => movementType;

	public int CurrentX => currentX;

	public int CurrentY => currentY;

	public UnitType TargetType => targetType;

	public uint TargetUID => targetUID;

	public string Unknown15 => ByteConverter.ToHexString(data, 15, 2);

	/// <summary>
	/// Found in alot of movement packets...
	/// </summary>
	public string Unknown17 => ByteConverter.ToHexString(data, 17, 4);

	public NPCMoveToTarget(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 1);
		movementType = data[5];
		currentX = BitConverter.ToUInt16(data, 6);
		currentY = BitConverter.ToUInt16(data, 8);
		targetType = (UnitType)data[10];
		targetUID = BitConverter.ToUInt32(data, 11);
	}
}
