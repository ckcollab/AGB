namespace D2Data;

public class ChargedSkillStat : SkillStat
{
	public readonly int Charges;

	public readonly int MaxCharges;

	public readonly uint ItemUID;

	public int Level => Value;

	public ChargedSkillStat(BaseStat stat, int level, int skill, int charges, int maxCharges, uint itemUID)
		: this(stat, level, (SkillType)skill, charges, maxCharges, itemUID)
	{
	}

	public ChargedSkillStat(BaseStat stat, int level, SkillType skill, int charges, int maxCharges, uint itemUID)
		: base(stat, skill, level)
	{
		Charges = charges;
		MaxCharges = maxCharges;
		ItemUID = itemUID;
	}

	public override StatBase Clone()
	{
		return new ChargedSkillStat(BaseStat, Level, Skill, Charges, MaxCharges, ItemUID);
	}

	public override string ToString()
	{
		return $"Level {Value} {Skill} ({Charges}/{MaxCharges} Charges)";
	}
}
