namespace D2Data;

public class SkillBonusStat : SkillStat
{
	public SkillBonusStat(BaseStat stat, int skill, int val)
		: base(stat, skill, val)
	{
	}

	public SkillBonusStat(BaseStat stat, SkillType skill, int val)
		: base(stat, skill, val)
	{
	}

	public override StatBase Clone()
	{
		return new SkillBonusStat(BaseStat, Skill, Value);
	}

	public override string ToString()
	{
		return $"+{Value} To {Skill}";
	}
}
