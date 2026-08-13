using System;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x18 - Character Upgrade Response
/// <para>Notifies client of the character upgrade result.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CharacterUpgradeRequest" />
/// </remarks>
public class CharacterUpgradeResponse : RSPacket
{
	protected RealmCharacterActionResult result;

	public RealmCharacterActionResult Result => result;

	public CharacterUpgradeResponse(byte[] data)
		: base(data)
	{
		result = (RealmCharacterActionResult)BitConverter.ToUInt32(data, 3);
	}
}
