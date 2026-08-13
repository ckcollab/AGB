using D2Data;

namespace D2Packets.D2Packets;

public class StatString
{
	public static void ParseD2StatString(byte[] data, int index, ref int clientVersion, ref BattleNetCharacter characterType, ref int characterLevel, ref CharacterFlags characterFlags, ref int characterAct, ref CharacterTitle characterTitle)
	{
		clientVersion = data[index];
		int bnc = data[index + 13] - 1;
		if (bnc < 0 || bnc > 6)
		{
			characterType = BattleNetCharacter.Unknown;
		}
		else
		{
			characterType = (BattleNetCharacter)bnc;
			if (CharactersInfo.Gender[(int)characterType])
			{
				characterFlags |= CharacterFlags.Female;
			}
		}
		characterLevel = data[index + 25];
		characterFlags |= (CharacterFlags)data[index + 26];
		int act = (data[index + 27] & 0x3E) >> 1;
		int title;
		if ((characterFlags & CharacterFlags.Expansion) == CharacterFlags.Expansion)
		{
			title = act / 5;
			act %= 5;
		}
		else
		{
			title = act / 4;
			act %= 4;
		}
		if (title == 3)
		{
			characterAct = 666;
		}
		else
		{
			characterAct = act + 1;
		}
		if ((characterFlags & CharacterFlags.Hardcore) == CharacterFlags.Hardcore)
		{
			title |= 4;
		}
		if ((characterFlags & CharacterFlags.Expansion) == CharacterFlags.Expansion)
		{
			title |= 0x20;
		}
		if ((characterFlags & CharacterFlags.Female) == CharacterFlags.Female)
		{
			title |= 0x100;
		}
		characterTitle = (CharacterTitle)title;
	}
}
