namespace D2Data;

public class AuraStat : SkillStat
{
	public AuraStat(BaseStat stat, int skill, int val)
		: base(stat, skill, val)
	{
	}

	public AuraStat(BaseStat stat, SkillType skill, int val)
		: base(stat, skill, val)
	{
	}

	public override StatBase Clone()
	{
		return new AuraStat(BaseStat, Skill, Value);
	}

	public override string ToString()
	{
		return $"Level {Value} {Skill} Aura";
	}
}
