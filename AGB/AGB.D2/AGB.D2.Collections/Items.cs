using System.Collections.Generic;
using AGB.Collections;
using D2Data;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class Items : ThreadSafeList<Item>
{
	private Game Game;

	public Items(Game game)
	{
		Game = game;
	}

	public Item Update(ItemAction action)
	{
		lock (PadLock)
		{
			Item item = ItemList.Find((Item i) => i.Uid == action.UID);
			if (item == null)
			{
				item = new Item(Game);
				ItemList.Add(item);
			}
			item.Initialize(action);
			return item;
		}
	}

	public void Remove(uint uid)
	{
		lock (PadLock)
		{
			Item obj = ItemList.Find((Item o) => o.Uid == uid);
			if (obj != null)
			{
				ItemList.Remove(obj);
			}
		}
	}

	public List<Item> GetFromContainer(ItemLocation container)
	{
		return ItemList.FindAll((Item i) => i.Action.Container == container);
	}

	public List<Item> GetFromShop()
	{
		lock (PadLock)
		{
			return ItemList.FindAll((Item i) => i.IsInShop);
		}
	}

	public void ClearShop()
	{
		lock (PadLock)
		{
			for (int i = ItemList.Count - 1; i > 0; i--)
			{
				if (ItemList[i].IsInShop)
				{
					Remove(ItemList[i]);
				}
			}
		}
	}
}
