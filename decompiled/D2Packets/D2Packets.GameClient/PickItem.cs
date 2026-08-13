using System;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x16 - PickItem
/// <para>Pick up an item from the ground.</para>
/// </summary>
public class PickItem : GCPacket
{
	protected uint requestID;

	protected uint uid;

	protected bool toCursor = false;

	public uint RequestID => requestID;

	public uint UID => uid;

	public bool ToCursor => toCursor;

	public PickItem(byte[] data)
		: base(data)
	{
		requestID = BitConverter.ToUInt32(data, 1);
		uid = BitConverter.ToUInt32(data, 5);
		if (data[9] == 1)
		{
			toCursor = true;
		}
	}

	public PickItem(uint uid, bool toCursor, uint requestID)
		: base(Build(uid, toCursor, requestID))
	{
		this.uid = uid;
		this.toCursor = toCursor;
		this.requestID = requestID;
	}

	public static byte[] Build(uint uid, bool toCursor, uint requestID)
	{
		return new byte[13]
		{
			22,
			(byte)requestID,
			(byte)(requestID >> 8),
			(byte)(requestID >> 16),
			(byte)(requestID >> 24),
			(byte)uid,
			(byte)(uid >> 8),
			(byte)(uid >> 16),
			(byte)(uid >> 24),
			(byte)(toCursor ? 1u : 0u),
			0,
			0,
			0
		};
	}
}
