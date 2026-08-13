using System;

namespace D2Packets.D2Packets;

/// <summary>
/// Exception throw when trying to write to a packet marked as completed.
/// </summary>
public class PacketNotWritableException : Exception
{
	public PacketNotWritableException()
		: base("Packets can only be written to in the first phase of the propagation!")
	{
	}
}
