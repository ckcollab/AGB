using System.Text;
using D2Data;

namespace AGB.D2.Net.D2.GC;

public class GameLogon : BasePacket
{
	private readonly uint ServerHash;

	private readonly ushort ServerToken;

	private readonly CharacterClass CharacterId;

	private readonly uint VersionByte = 12u;

	private readonly uint unknown = 3982347344u;

	private readonly uint unknown2 = 2443516342u;

	private readonly byte unknwon3 = 0;

	private readonly byte[] CharacterName = new byte[16]
	{
		0, 0, 0, 158, 174, 212, 119, 154, 24, 174,
		111, 75, 0, 0, 0, 0
	};

	public byte[] Data;

	public GameLogon(uint serverHash, ushort serverToken, CharacterClass CharClass, string CharacterName)
		: base(104)
	{
		ServerHash = serverHash;
		ServerToken = serverToken;
		CharacterId = CharClass;
		StringBuilder sb = new StringBuilder(CharacterName);
		for (int currIndex = 0; currIndex < sb.Length + 1; currIndex++)
		{
			if (currIndex == sb.Length)
			{
				this.CharacterName[currIndex] = 0;
			}
			else
			{
				this.CharacterName[currIndex] = (byte)sb[currIndex];
			}
		}
		Data = GetData();
	}

	public override byte[] GetData()
	{
		InsertUInt32(ServerHash);
		InsertUInt16(ServerToken);
		InsertByte((byte)CharacterId);
		InsertUInt32(VersionByte);
		InsertUInt32(unknown);
		InsertUInt32(unknown2);
		InsertByte(unknwon3);
		InsertByteArray(CharacterName);
		return base.GetData();
	}
}
