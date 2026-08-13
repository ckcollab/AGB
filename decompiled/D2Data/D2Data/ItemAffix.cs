using System;

namespace D2Data;

public class ItemAffix
{
	public readonly ItemAffixType Type;

	public readonly int Index;

	public string Name
	{
		get
		{
			if ((Type & ItemAffixType.Superior) == ItemAffixType.Superior)
			{
				return ItemNames.SuperiorPrefix;
			}
			if ((Type & ItemAffixType.Inferior) == ItemAffixType.Inferior)
			{
				return ItemNames.InferiorPrefix[Index];
			}
			if ((Type & ItemAffixType.Magic) == ItemAffixType.Magic)
			{
				if (Index == 0)
				{
					return null;
				}
				if ((Type & ItemAffixType.Prefix) == ItemAffixType.Prefix)
				{
					return ((MagicPrefixType)Index).ToString();
				}
				return ((MagicSuffixType)Index).ToString();
			}
			if ((Type & ItemAffixType.Rare) == ItemAffixType.Rare)
			{
				return ItemNames.RareAffix[Index];
			}
			return null;
		}
	}

	public ItemAffix(ItemAffixType type, int index)
	{
		Index = index;
		Type = type;
		if (Type < (ItemAffixType)3)
		{
			throw new ArgumentException();
		}
		if ((Type & (ItemAffixType)3) != 0)
		{
			return;
		}
		switch (Type)
		{
		case ItemAffixType.Superior:
			Type |= ItemAffixType.Prefix;
			break;
		case ItemAffixType.Inferior:
			Type |= ItemAffixType.Prefix;
			break;
		case ItemAffixType.Magic:
			throw new ArgumentException();
		case ItemAffixType.Rare:
			if (index < ItemNames.RarePrefixOffset)
			{
				Type |= ItemAffixType.Suffix;
			}
			else
			{
				Type |= ItemAffixType.Prefix;
			}
			break;
		default:
			throw new ArgumentException();
		}
	}

	public override string ToString()
	{
		return Name;
	}
}
