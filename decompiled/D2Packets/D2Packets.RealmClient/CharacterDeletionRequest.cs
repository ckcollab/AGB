using System;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x0A - Character Deletion Request
/// <para>Request deletion of a realm character in the current account.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CharacterDeletionResponse" />
/// </remarks>
public class CharacterDeletionRequest : RCPacket
{
	protected uint cookie;

	protected string name;

	public uint Cookie => cookie;

	public string Name => name;

	public CharacterDeletionRequest(byte[] data)
		: base(data)
	{
		cookie = BitConverter.ToUInt16(data, 3);
		name = ByteConverter.GetNullString(data, 5);
	}
}
