using System;
using AGB.D2.Collections;
using D2Data;

namespace AGB.D2;

public class Mercenary : NPC
{
	public Items Items;

	public Mercenary(Game game)
		: base(game)
	{
		IsMercenary = true;
		Items = new Items(game);
	}

	public bool HasItem(uint uid)
	{
		return Items.Find((Item item) => item.Uid == uid) != null;
	}

	public Item GetItemAt(EquipmentLocation location)
	{
		return Items.Find((Item item) => item.Action.EquipmentLocation == location);
	}

	public override string ToString()
	{
		return "Life: " + base.Life + "/" + MaxLife + Environment.NewLine + "Mana: " + base.Mana + "/" + MaxMana + Environment.NewLine + "UID: " + Uid + Environment.NewLine + "Coords: " + X + ", " + Y + Environment.NewLine + Environment.NewLine;
	}
}
