using System;
using D2Data;
using ETUtils;

namespace D2Packets.GameServer;

/// <summary>
/// Game Server Packet 0x81 - Assign Merc
/// <para>Assigns a mercenary when hired or resurrected (also sent for party members' mercs).</para>
/// </summary>
public class AssignMerc : GSPacket
{
	protected NPCClass id;

	protected uint ownerUID;

	protected uint uid;

	public NPCClass ID => id;

	public uint OwnerUID => ownerUID;

	public uint UID => uid;

	public string Unknown1 => ByteConverter.ToHexString(data, 1, 1);

	public string Unknown5 => ByteConverter.ToHexString(data, 12, 8);

	public AssignMerc(byte[] data)
		: base(data)
	{
		id = (NPCClass)BitConverter.ToUInt16(data, 2);
		ownerUID = BitConverter.ToUInt32(data, 4);
		uid = BitConverter.ToUInt32(data, 8);
	}
}
