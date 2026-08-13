namespace D2Data;

public class UnsignedStatParam : UnsignedStat
{
	public uint Param;

	public UnsignedStatParam(BaseStat stat, uint val, uint param)
		: base(stat, val)
	{
		Param = param;
	}

	public override StatBase Clone()
	{
		return new UnsignedStatParam(BaseStat, Value, Param);
	}

	public override string ToString()
	{
		return $"{BaseStat}: {Value}, {Param}";
	}
}
