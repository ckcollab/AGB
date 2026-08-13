using System;
using D2Data;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x51 - Assign Game Object
/// <para>Identifies objects when they come within range.</para>
/// </summary>
public class AssignGameObject : GSPacket
{
	protected GameObjectClass objectID;

	protected GameObjectInteractType interactType;

	protected GameObjectMode objectMode;

	protected uint uid;

	protected int x;

	protected int y;

	protected AreaLevel destination;

	public GameObjectClass ObjectID => objectID;

	public GameObjectInteractType InteractType => interactType;

	public GameObjectMode ObjectMode => objectMode;

	public uint UID => uid;

	public int X => x;

	public int Y => y;

	public AreaLevel Destination => destination;

	public AssignGameObject(byte[] data)
		: base(data)
	{
		uid = BitConverter.ToUInt32(data, 2);
		objectID = (GameObjectClass)BitConverter.ToUInt16(data, 6);
		x = BitConverter.ToUInt16(data, 8);
		y = BitConverter.ToUInt16(data, 10);
		objectMode = (GameObjectMode)data[12];
		if (objectID == GameObjectClass.TownPortal)
		{
			interactType = GameObjectInteractType.TownPortal;
			destination = (AreaLevel)data[13];
		}
		else
		{
			interactType = (GameObjectInteractType)data[13];
			destination = AreaLevel.None;
		}
	}
}
