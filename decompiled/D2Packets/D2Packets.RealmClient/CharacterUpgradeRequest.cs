using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x18 - Character Upgrade Request
/// <para>Requests upgrading a character from classic to expansion.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CharacterUpgradeResponse" />
/// </remarks>
public class CharacterUpgradeRequest : RCPacket
{
	protected readonly string name;

	public string Name => name;

	public CharacterUpgradeRequest(byte[] data)
		: base(data)
	{
		name = ByteConverter.GetNullString(data, 3);
	}
}
