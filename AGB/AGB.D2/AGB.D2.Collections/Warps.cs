using System;
using System.Threading;
using AGB.Collections;
using D2Data;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class Warps : ThreadSafeList<Warp>
{
	private Game Game;

	public Warps(Game game)
	{
		Game = game;
	}

	public void Update(AssignWarp assignWarp)
	{
		lock (PadLock)
		{
			Warp warp = ItemList.Find((Warp w) => w.Uid == assignWarp.UID);
			if (warp == null)
			{
				warp = new Warp(Game);
				ItemList.Add(warp);
			}
			warp.Uid = assignWarp.UID;
			warp.X = assignWarp.X;
			warp.Y = assignWarp.Y;
			warp.UnitType = assignWarp.UnitType;
			warp.Id = assignWarp.ID;
		}
	}

	public void Remove(uint uid)
	{
		lock (PadLock)
		{
			Warp warp = ItemList.Find((Warp w) => w.Uid == uid);
			if (warp != null)
			{
				ItemList.Remove(warp);
			}
		}
	}

	public Warp Find(WarpType id, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			Warp w = Find((Warp o) => o.Id == id);
			if (w != null)
			{
				return w;
			}
			Thread.Sleep(100);
		}
		return null;
	}
}
