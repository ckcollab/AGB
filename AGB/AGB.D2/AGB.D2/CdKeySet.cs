namespace AGB.D2;

public class CdKeySet
{
	public string Classic;

	public string Expansion;

	public CdKeySet()
	{
	}

	public CdKeySet(string classic, string expansion)
	{
		Classic = classic;
		Expansion = expansion;
	}

	public override string ToString()
	{
		return "Classic = " + Classic + "; Expansion = " + Expansion;
	}
}
