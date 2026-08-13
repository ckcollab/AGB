namespace D2Data;

public class ItemPropertyEntry
{
	public readonly BaseStat Stat;

	public readonly int Value;

	public readonly int Set;

	public readonly int Func;

	public ItemPropertyEntry(int Set, int Val, int Func, int Stat)
	{
		this.Set = Set;
		Value = Val;
		this.Func = Func;
		this.Stat = BaseStat.Get(Stat);
	}
}
