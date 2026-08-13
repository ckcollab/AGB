namespace D2Data;

public class SkillTabBonusStat : SignedStat
{
	public readonly CharacterClass Class;

	public readonly SkillTab Tab;

	public readonly int TabNumber;

	public readonly int Unknown = -1;

	public SkillTabBonusStat(BaseStat stat, int tab, int charClass, int val)
		: base(stat, val)
	{
		Class = (CharacterClass)charClass;
		Tab = (SkillTab)((charClass << 3) | tab);
		TabNumber = tab;
	}

	public SkillTabBonusStat(BaseStat stat, int tab, int charClass, int unknown, int val)
		: base(stat, val)
	{
		Class = (CharacterClass)charClass;
		Tab = (SkillTab)((charClass << 3) | tab);
		TabNumber = tab;
		Unknown = unknown;
	}

	public SkillTabBonusStat(BaseStat stat, SkillTab tab, CharacterClass charClass, int val)
		: base(stat, val)
	{
		Class = charClass;
		Tab = tab;
		TabNumber = (int)(tab & SkillTab.JavelinAndSpear);
	}

	public SkillTabBonusStat(BaseStat stat, SkillTab tab, CharacterClass charClass, int unknown, int val)
		: base(stat, val)
	{
		Class = charClass;
		Tab = tab;
		TabNumber = (int)(tab & SkillTab.JavelinAndSpear);
		Unknown = unknown;
	}

	public override StatBase Clone()
	{
		return new SkillTabBonusStat(BaseStat, Tab, Class, Unknown, Value);
	}

	public override string ToString()
	{
		if (Unknown > 0)
		{
			return $"+{Value} to {Class} {Tab} Skills ({Unknown})";
		}
		return $"+{Value} to {Class} {Tab} Skills";
	}
}
