namespace D2Data;

public class UnsignedStat : StatBase
{
	public uint Value;

	public UnsignedStat(StatType stat, uint val)
		: base(BaseStat.Get(stat))
	{
		Value = val;
	}

	public UnsignedStat(BaseStat stat, uint val)
		: base(stat)
	{
		Value = val;
	}

	public override StatBase Clone()
	{
		return new UnsignedStat(BaseStat, Value);
	}

	public override string ToString()
	{
		return $"{BaseStat}: {Value}";
	}
}
