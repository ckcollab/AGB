using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x29 - Embed Item
/// <para>Put an item into a container item (e.g. scroll in tome.)</para>
/// </summary>
public class EmbedItem : GCPacket
{
	protected uint subjectUID;

	protected uint objectUID;

	public uint SubjectUID => subjectUID;

	public uint ObjectUID => objectUID;

	public EmbedItem(byte[] data)
		: base(data)
	{
		subjectUID = BitConverter.ToUInt32(data, 1);
		objectUID = BitConverter.ToUInt32(data, 5);
	}

	public EmbedItem(uint subjectUID, uint objectUID)
		: base(Build(subjectUID, objectUID))
	{
		this.subjectUID = subjectUID;
		this.objectUID = objectUID;
	}

	public static byte[] Build(uint subjectUID, uint objectUID)
	{
		return new byte[9]
		{
			41,
			(byte)subjectUID,
			(byte)(subjectUID >> 8),
			(byte)(subjectUID >> 16),
			(byte)(subjectUID >> 24),
			(byte)objectUID,
			(byte)(objectUID >> 8),
			(byte)(objectUID >> 16),
			(byte)(objectUID >> 24)
		};
	}
}
