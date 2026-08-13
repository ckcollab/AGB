namespace D2Data;

public class ClassSkillsBonusStat : SignedStat
{
	public readonly CharacterClass Class;

	public ClassSkillsBonusStat(BaseStat stat, int charClass, int val)
		: base(stat, val)
	{
		Class = (CharacterClass)charClass;
	}

	public ClassSkillsBonusStat(BaseStat stat, CharacterClass charClass, int val)
		: base(stat, val)
	{
		Class = charClass;
	}

	public override StatBase Clone()
	{
		return new ClassSkillsBonusStat(BaseStat, Class, Value);
	}

	public override string ToString()
	{
		return $"+{Value} To {Class} Skills";
	}
}
