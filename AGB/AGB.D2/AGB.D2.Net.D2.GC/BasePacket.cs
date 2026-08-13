using System;
using System.Collections.Generic;
using MBNCSUtil;

namespace AGB.D2.Net.D2.GC;

public class BasePacket : DataBuffer
{
	private byte? packetid = null;

	public byte? PacketID
	{
		get
		{
			return packetid;
		}
		set
		{
			packetid = value;
		}
	}

	public BasePacket(byte? PacketId)
	{
		packetid = PacketId;
	}

	public BasePacket(byte? PacketId, IEnumerable<byte> HeaderLessData)
	{
		packetid = PacketId;
		foreach (byte b in HeaderLessData)
		{
			InsertByte(b);
		}
	}

	public BasePacket(IEnumerable<byte> HeaderLessData)
	{
		foreach (byte b in HeaderLessData)
		{
			InsertByte(b);
		}
	}

	public override byte[] GetData()
	{
		if (!((int?)packetid).HasValue)
		{
			throw new ApplicationException("Attemp to build a packet without the packet ID byte defined");
		}
		byte[] RawData = base.GetData();
		byte[] retVal = new byte[RawData.Length + 1];
		retVal[0] = packetid.Value;
		for (int i = 0; i < RawData.Length; i++)
		{
			retVal[i + 1] = RawData[i];
		}
		return retVal;
	}
}
