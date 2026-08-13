using D2Data;
using ETUtils;

namespace D2Packets.D2Packets;

public class CharacterBaseInfo
{
	public string Name;

	public CharacterClass Class;

	public int Level;

	public CharacterBaseInfo(string name, int charClass, int level)
	{
		Name = name;
		Class = (CharacterClass)charClass;
		Level = level;
	}

	public override string ToString()
	{
		return StringUtils.ToInfoString((object)this);
	}
}
