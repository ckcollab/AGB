namespace AGB.D2;

internal class CdKeySetInstance : CdKeySet
{
	public int Instances = 0;

	public int AllowedInstances = 1;

	public CdKeySetInstance(CdKeySet cdKeySet)
		: base(cdKeySet.Classic, cdKeySet.Expansion)
	{
	}

	public CdKeySetInstance(string classic, string expansion)
		: base(classic, expansion)
	{
	}
}
