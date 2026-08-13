using System;
using System.Threading;
using D2Data;
using D2Packets.D2Packets;
using D2Packets.GameClient;
using D2Packets.GameServer;

namespace AGB.D2;

public class Potion : Unit
{
	private Item Item;

	public int Potency;

	public PotionType Type;

	public Potion(Game game, Item item)
		: base(game)
	{
		Item = item;
		Uid = Item.Uid;
		X = Item.X;
		Y = Item.Y;
		BaseMiscItem baseItem = Item.Action.BaseItem as BaseMiscItem;
		Potency = baseItem.Calc1;
		switch (baseItem.Stat1)
		{
		case StatType.ManaRecovery:
			Type = PotionType.Mana;
			break;
		case StatType.LifeRegen:
			Type = PotionType.Health;
			break;
		case StatType.Life:
			Potency += 1000;
			Type = PotionType.Rejuvenation;
			break;
		default:
			throw new ArgumentException("Item is not a potion...?");
		}
	}

	public bool Drink(bool toMerc, int timeOut)
	{
		bool drank = false;
		PacketEventHandler worldItemActionDelg = delegate(D2Packet packet)
		{
			WorldItemAction worldItemAction = new WorldItemAction(packet.Data);
			if (worldItemAction.UID == Uid)
			{
				drank = true;
			}
		};
		Game.Socket.PacketHandler.AddAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		DateTime watchstart = DateTime.Now;
		while (DateTime.Now.Subtract(watchstart).TotalMilliseconds < (double)timeOut)
		{
			Game.Socket.Game.Send(new UseBeltItem(Uid, toMerc).Data);
			if (drank)
			{
				break;
			}
			Thread.Sleep(1000);
		}
		Game.Socket.PacketHandler.RemoveAsyncListener(GameServerPacket.WorldItemAction, worldItemActionDelg);
		return drank;
	}

	public static int Compare(Potion p1, Potion p2)
	{
		if (p1.Potency < p2.Potency)
		{
			return -1;
		}
		if (p1.Potency == p2.Potency)
		{
			return 0;
		}
		return 1;
	}
}
