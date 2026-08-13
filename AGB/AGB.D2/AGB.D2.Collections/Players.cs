using System.Collections.Generic;
using AGB.Collections;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class Players : ThreadSafeList<Player>
{
	private Game Game;

	public Players(Game game)
	{
		Game = game;
	}

	public Player Update(AssignPlayer assignPlayer)
	{
		lock (PadLock)
		{
			Player player = ItemList.Find((Player p) => p.Uid == assignPlayer.UID);
			if (player == null)
			{
				player = new Player(Game);
				ItemList.Add(player);
			}
			player.Class = assignPlayer.Class;
			player.X = assignPlayer.X;
			player.Y = assignPlayer.Y;
			player.Name = assignPlayer.Name;
			player.Uid = assignPlayer.UID;
			return player;
		}
	}

	public void Remove(uint uid)
	{
		lock (PadLock)
		{
			for (int i = 0; i < ItemList.Count; i++)
			{
				if (ItemList[i].Uid == uid)
				{
					Remove(ItemList[i]);
					break;
				}
			}
		}
	}

	public List<Player> Find(string name)
	{
		lock (PadLock)
		{
			return ItemList.FindAll((Player p) => p.Name == name);
		}
	}

	public Player Find(uint uid)
	{
		lock (PadLock)
		{
			return ItemList.Find((Player p) => p.Uid == uid);
		}
	}
}
