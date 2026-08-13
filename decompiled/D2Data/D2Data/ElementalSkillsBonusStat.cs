namespace D2Data;

public class ElementalSkillsBonusStat : SignedStat
{
	public readonly DamageType Element;

	public ElementalSkillsBonusStat(BaseStat stat, int element, int val)
		: base(stat, val)
	{
		Element = (DamageType)element;
	}

	public ElementalSkillsBonusStat(BaseStat stat, DamageType element, int val)
		: base(stat, val)
	{
		Element = element;
	}

	public override StatBase Clone()
	{
		return new ElementalSkillsBonusStat(BaseStat, Element, Value);
	}

	public override string ToString()
	{
		return $"+{Value} To {Element} Skills";
	}
}
