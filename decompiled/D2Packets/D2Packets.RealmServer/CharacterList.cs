using System;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.RealmServer;

/// <summary>
/// Realm Server Packet 0x19 - Character List
/// <para>Provides a list of characters for the current account.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.RealmClient.CharacterListRequest" />
/// </remarks>
public class CharacterList : RSPacket
{
	protected uint requested;

	protected uint total;

	protected uint listed;

	protected CharacterInfo[] characters;

	public uint Requested => requested;

	public uint Total => total;

	public uint Listed => listed;

	public CharacterInfo[] Characters => characters;

	public CharacterList(byte[] data)
		: base(data)
	{
		requested = BitConverter.ToUInt16(data, 3);
		total = BitConverter.ToUInt32(data, 5);
		listed = BitConverter.ToUInt16(data, 9);
		characters = new CharacterInfo[listed];
		int index = 11;
		for (int i = 0; i < Listed; i++)
		{
			if (index >= data.Length)
			{
				break;
			}
			characters[i] = new CharacterInfo();
			characters[i].Expires = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, index));
			characters[i].Name = ByteConverter.GetNullString(data, index += 4);
			index += characters[i].Name.Length + 1;
			StatString.ParseD2StatString(data, index, ref characters[i].ClientVersion, ref characters[i].Class, ref characters[i].Level, ref characters[i].Flags, ref characters[i].Act, ref characters[i].Title);
			index = ByteConverter.GetBytePosition(data, 0, index) + 1;
		}
	}
}
