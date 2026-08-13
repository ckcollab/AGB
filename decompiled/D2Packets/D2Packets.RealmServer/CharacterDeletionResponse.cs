using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x0A - Character Deletion Response
/// <para>Notifies client of the character deletion result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CharacterDeletionRequest" />
/// </remarks>
public class CharacterDeletionResponse : RSPacket
{
	protected RealmCharacterActionResult result;

	public RealmCharacterActionResult Result => result;

	public CharacterDeletionResponse(byte[] data)
		: base(data)
	{
		result = (RealmCharacterActionResult)BitConverter.ToUInt32(data, 3);
	}
}
