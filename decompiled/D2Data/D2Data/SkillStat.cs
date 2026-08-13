namespace D2Data;

public class SkillStat : SignedStat
{
	public readonly SkillType Skill;

	public SkillStat(BaseStat stat, int skill, int val)
		: base(stat, val)
	{
		Skill = (SkillType)skill;
	}

	public SkillStat(BaseStat stat, SkillType skill, int val)
		: base(stat, val)
	{
		Skill = skill;
	}

	public override StatBase Clone()
	{
		return new SkillStat(BaseStat, Skill, Value);
	}
}
