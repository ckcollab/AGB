namespace D2Data;

public class SkillOnEventStat : SkillStat
{
	public readonly int Chance;

	public readonly ItemEventType Event;

	public int Level => Value;

	public SkillOnEventStat(BaseStat stat, int level, int skill, int chance)
		: this(stat, level, (SkillType)skill, chance)
	{
	}

	public SkillOnEventStat(BaseStat stat, int level, SkillType skill, int chance)
		: base(stat, skill, level)
	{
		Chance = chance;
		Event = stat.ItemEvent1 | stat.ItemEvent2;
	}

	public override StatBase Clone()
	{
		return new SkillOnEventStat(BaseStat, Level, Skill, Chance);
	}

	public override string ToString()
	{
		return $"{Chance}% Chance To Cast Level {Value} {Skill} {BaseStat.ToString().Substring(6)}";
	}
}
