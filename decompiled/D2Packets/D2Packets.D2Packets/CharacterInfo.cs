using System;
using D2Data;
using ETUtils;

namespace D2Packets.D2Packets;

public class CharacterInfo
{
	public string Name;

	public BattleNetCharacter Class;

	public int Level;

	public CharacterFlags Flags;

	public CharacterTitle Title;

	public int Act;

	public DateTime Expires;

	public int ClientVersion;

	public override string ToString()
	{
		return StringUtils.ToInfoString((object)this);
	}
}
