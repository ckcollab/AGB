using System;
using D2Data;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x02 - Character Creation Request
/// <para>Request creation of a new realm character in the current account.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.CharacterCreationResponse" />
/// </remarks>
public class CharacterCreationRequest : RCPacket
{
	protected CharacterClass charClass;

	protected CharacterFlags flags;

	protected string name;

	public CharacterClass Class => charClass;

	public CharacterFlags Flags => flags;

	public string Name => name;

	public CharacterCreationRequest(byte[] data)
		: base(data)
	{
		charClass = (CharacterClass)BitConverter.ToUInt32(data, 3);
		flags = (CharacterFlags)BitConverter.ToUInt16(data, 7);
		name = ByteConverter.GetNullString(data, 9);
	}
}
