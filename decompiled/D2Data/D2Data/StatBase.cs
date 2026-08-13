namespace D2Data;

public class StatBase
{
	public readonly BaseStat BaseStat;

	public StatBase(BaseStat stat)
	{
		BaseStat = stat;
	}

	public virtual StatBase Clone()
	{
		return new StatBase(BaseStat);
	}

	public override string ToString()
	{
		return BaseStat.ToString();
	}
}
