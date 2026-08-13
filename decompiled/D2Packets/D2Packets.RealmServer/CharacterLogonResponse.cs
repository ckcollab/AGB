using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x07 - Character Logon Response
/// <para>Notifies client of the character logon result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CharacterLogonRequest" />
/// </remarks>
public class CharacterLogonResponse : RSPacket
{
	protected RealmCharacterActionResult result;

	public RealmCharacterActionResult Result => result;

	public CharacterLogonResponse(byte[] data)
		: base(data)
	{
		result = (RealmCharacterActionResult)BitConverter.ToUInt32(data, 3);
	}
}
