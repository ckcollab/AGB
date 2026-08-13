using System;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x19 - Character List Request
/// <para>Request a list of characters for the current account.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CharacterList" />
/// </remarks>
public class CharacterListRequest : RCPacket
{
	protected int number;

	public int Number => number;

	public CharacterListRequest(byte[] data)
		: base(data)
	{
		number = BitConverter.ToInt32(data, 3);
	}
}
