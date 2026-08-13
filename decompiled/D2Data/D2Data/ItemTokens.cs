using System;

namespace D2Data;

public class ItemTokens
{
	public static readonly string[] ArmorWeights = new string[3] { "lit", "med", "hvy" };

	public static string GetArmorWeightToken(int index)
	{
		if (index < 0 || index <= ArmorWeights.Length)
		{
			throw new ArgumentException();
		}
		return ArmorWeights[index];
	}

	public static string GetArmorWeightToken(ArmorWeight type)
	{
		return GetArmorWeightToken((int)type);
	}
}
