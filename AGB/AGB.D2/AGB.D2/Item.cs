using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;

namespace AGB.D2;

public class Item : Unit
{
	private delegate void PickUpAction();

	public ItemAction Action;

	public Dictionary<string, int> SimpleMods = new Dictionary<string, int>();

	public Dictionary<string, int> SimpleStats = new Dictionary<string, int>();

	public int Sockets
	{
		get
		{
			StatBase[] stats = Action.Stats;
			foreach (StatBase stat in stats)
			{
				if (stat.BaseStat.Type == StatType.Sockets)
				{
					return ((SignedStat)stat).Value;
				}
			}
			return 0;
		}
	}

	public bool IsIdentified => (Convert.ToInt32(Action.Flags) & 0x10) == 16;

	public bool IsInShop => Action.Container == ItemLocation.ArmorTab || Action.Container == ItemLocation.WeaponTab1 || Action.Container == ItemLocation.WeaponTab2 || Action.Container == ItemLocation.MiscTab;

	public new uint Uid => Action.UID;

	public new int X => Action.X;

	public new int Y => Action.Y;

	public Item(Game game)
		: base(game)
	{
	}

	public void Initialize(ItemAction packet)
	{
		Action = packet;
		SimpleStats = MakeStatTypesSimple(Action.Stats);
		SimpleMods = MakeStatTypesSimple(Action.Mods);
		if (SimpleMods.ContainsKey("LightResist") && SimpleMods.ContainsKey("ColdResist") && SimpleMods.ContainsKey("FireResist") && SimpleMods.ContainsKey("PoisonResist"))
		{
			int lowest = SimpleMods["LightResist"];
			if (SimpleMods["ColdResist"] < lowest)
			{
				lowest = SimpleMods["ColdResist"];
			}
			if (SimpleMods["FireResist"] < lowest)
			{
				lowest = SimpleMods["FireResist"];
			}
			if (SimpleMods["PoisonResist"] < lowest)
			{
				lowest = SimpleMods["PoisonResist"];
			}
			SimpleMods.Add("ResistAll", lowest);
		}
	}

	private Dictionary<string, int> MakeStatTypesSimple(StatBase[] stats)
	{
		Dictionary<string, int> simpleStats = new Dictionary<string, int>();
		foreach (StatBase stat in stats)
		{
			int value = (stat.BaseStat.Signed ? ((SignedStat)stat).Value : ((int)((UnsignedStat)stat).Value));
			string statName = stat.BaseStat.Type.ToString();
			switch (stat.BaseStat.Type)
			{
			case StatType.ClassSkillsBonus:
				statName = (stat as ClassSkillsBonusStat).Class.ToString() + "Skills";
				break;
			case StatType.ArmorClass:
				statName = "Defense";
				break;
			}
			if (!simpleStats.ContainsKey(statName))
			{
				simpleStats.Add(statName, value);
			}
		}
		return simpleStats;
	}

	public static bool IsStatKeyword(string statName)
	{
		string text = statName;
		if (text != null && text == "ResistAll")
		{
			return true;
		}
		switch (statName)
		{
		case "AmazonSkills":
		case "AssassinSkills":
		case "PaladinSkills":
		case "BarbarianSkills":
		case "SorceressSkills":
		case "NecromancerSkills":
		case "DruidSkills":
			statName = "ClassSkillsBonus";
			break;
		case "Defense":
			statName = "ArmorClass";
			break;
		}
		return Enum.IsDefined(typeof(StatType), statName);
	}

	public bool ToCursor(int timeOut)
	{
		PickUpAction action = delegate
		{
			if (Action.Container == ItemLocation.Unspecified || Action.Destination == ItemDestination.Ground || Action.Container == ItemLocation.Ground)
			{
				Game.Socket.Game.Send(new PickItem(Uid, toCursor: true, 4u).Data);
			}
			else
			{
				switch (Action.Container)
				{
				case ItemLocation.Inventory:
				case ItemLocation.Cube:
				case ItemLocation.Stash:
					Game.Socket.Game.Send(new PickItemFromContainer(Uid).Data);
					break;
				case ItemLocation.Belt:
					Game.Socket.Game.Send(new RemoveBeltItem(Uid).Data);
					break;
				default:
					throw new ArgumentException("I don't understand how to pick the item up where it's currently located, in the " + Action.Container);
				}
			}
		};
		return WaitForAction(action, timeOut);
	}

	public bool ToContainer(ItemLocation container, int timeOut)
	{
		if (Game.Hero.ItemOnCursor.Uid != Uid)
		{
			return false;
		}
		PickUpAction action = delegate
		{
			switch (container)
			{
			case ItemLocation.Ground:
				Game.Socket.Game.Send(new DropItem(Uid).Data);
				break;
			case ItemLocation.Cube:
			{
				Point point2 = Game.Hero.Cube.FindSpaceFor(this);
				if (point2.X == -1 && point2.Y == -1)
				{
					throw new ArgumentException(string.Concat("The ", container, " container is full!"));
				}
				Game.Socket.Game.Send(new DropItemToContainer(Uid, ItemContainerGC.Cube, point2.X, point2.Y).Data);
				break;
			}
			case ItemLocation.Inventory:
			{
				Point point3 = Game.Hero.Inventory.FindSpaceFor(this);
				if (point3.X == -1 && point3.Y == -1)
				{
					throw new ArgumentException(string.Concat("The ", container, " container is full!"));
				}
				Game.Socket.Game.Send(new DropItemToContainer(Uid, ItemContainerGC.Inventory, point3.X, point3.Y).Data);
				break;
			}
			case ItemLocation.Stash:
			{
				Point point = Game.Hero.Stash.FindSpaceFor(this);
				if (point.X == -1 && point.Y == -1)
				{
					throw new ArgumentException(string.Concat("The ", container, " container is full!"));
				}
				Game.Socket.Game.Send(new DropItemToContainer(Uid, ItemContainerGC.Stash, point.X, point.Y).Data);
				break;
			}
			case ItemLocation.Belt:
				Game.Socket.Game.Send(new RemoveBeltItem(Uid).Data);
				break;
			default:
				throw new ArgumentException("I don't understand how to place the item in the " + Action.Container);
			}
		};
		return WaitForAction(action, timeOut);
	}

	private bool WaitForAction(PickUpAction action, int timeout)
	{
		bool receivedValidAction = false;
		PacketEventHandler worldItemActionDelg = delegate(D2Packet packet)
		{
			WorldItemAction worldItemAction = new WorldItemAction(packet.Data);
			if (worldItemAction.UID == Uid)
			{
				receivedValidAction = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		PacketEventHandler ownedItemActionDelg = delegate(D2Packet packet)
		{
			OwnedItemAction ownedItemAction = new OwnedItemAction(packet.Data);
			if (ownedItemAction.UID == Uid)
			{
				receivedValidAction = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.OwnedItemAction, ownedItemActionDelg);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeout)
		{
			action();
			if (receivedValidAction)
			{
				break;
			}
			Thread.Sleep(10);
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.OwnedItemAction, ownedItemActionDelg);
		return receivedValidAction;
	}

	public void Interact()
	{
		if (Action.Container == ItemLocation.Inventory)
		{
			Game.Socket.Game.Send(new UseContainerItem(Uid, Game.Hero.X, Game.Hero.Y).Data);
		}
		if (Action.Container == ItemLocation.Belt)
		{
			Game.Socket.Game.Send(new UseBeltItem(Uid, toMerc: false).Data);
		}
	}
}
