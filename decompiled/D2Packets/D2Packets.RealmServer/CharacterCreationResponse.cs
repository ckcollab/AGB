using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x02 - Character Creation Response
/// <para>Notifies client of the character creation result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CharacterCreationRequest" />
/// </remarks>
public class CharacterCreationResponse : RSPacket
{
	protected RealmCharacterActionResult result;

	public RealmCharacterActionResult Result => result;

	public CharacterCreationResponse(byte[] data)
		: base(data)
	{
		result = (RealmCharacterActionResult)BitConverter.ToUInt32(data, 3);
	}
}
