using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x07 - Character Logon Request
/// <para>Requests picking a character (success bringing you to lobby...)</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CharacterLogonResponse" />
/// </remarks>
public class CharacterLogonRequest : RCPacket
{
	protected string name;

	public string Name => name;

	public CharacterLogonRequest(byte[] data)
		: base(data)
	{
		name = ByteConverter.GetNullString(data, 3);
	}
}
