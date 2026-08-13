using System;
using System.Collections.Generic;
using System.Threading;
using AGB.D2.Net;
using AGB.D2.Net.Packets;
using D2Data;

namespace AGB.D2;

public class MapManager
{
	private object PadLock = new object();

	private Dictionary<AreaLevel, Map> Maps;

	private Game Game;

	private Character Character;

	private bool HasSeedReceivingFinished = false;

	public MapManager(Game game)
	{
		Game = game;
		Character = Game.Profile.Character;
		Maps = new Dictionary<AreaLevel, Map>();
		Game.SeedReceived += SeedReceived;
		lock (PadLock)
		{
			Map harrogath = new Map(AreaLevel.Harrogath, new List<Room>
			{
				new Room
				{
					Id1 = 863,
					Id2 = 0
				}
			}, 5000, 5000, 200, 200);
			Maps.Add(AreaLevel.Harrogath, harrogath);
			Map nihlathaksTemple = new Map(AreaLevel.NihlathaksTemple, new List<Room>
			{
				new Room
				{
					Id1 = 1088,
					Id2 = 0
				}
			}, 10000, 13180, 110, 135);
			Maps.Add(AreaLevel.NihlathaksTemple, nihlathaksTemple);
		}
	}

	private void SeedReceived(Game game)
	{
		if (!HasSeedReceivingFinished)
		{
			AgbSocket.Instance.SetNewGameInfo(Character, game.Seed, 0, game.Difficulty, 15000);
			HasSeedReceivingFinished = true;
		}
	}

	public void Clear()
	{
		lock (PadLock)
		{
			HasSeedReceivingFinished = false;
			List<AreaLevel> mapsToRemove = new List<AreaLevel>();
			foreach (AreaLevel level in Maps.Keys)
			{
				AreaLevel areaLevel = level;
				if (areaLevel != AreaLevel.Harrogath && areaLevel != AreaLevel.NihlathaksTemple)
				{
					mapsToRemove.Add(level);
				}
			}
			foreach (AreaLevel level in mapsToRemove)
			{
				Maps.Remove(level);
			}
		}
	}

	public Map GetMap(AreaLevel level)
	{
		DateTime start = DateTime.Now;
		while (!HasSeedReceivingFinished)
		{
			if (DateTime.Now.Subtract(start).TotalMilliseconds > 15000.0)
			{
				return null;
			}
			Thread.Sleep(1);
		}
		lock (PadLock)
		{
			if (level == AreaLevel.None)
			{
				int alertme = 1;
				alertme++;
			}
			if (Maps.ContainsKey(level))
			{
				if (Maps[level].Collisions == null)
				{
					Maps[level].LoadCollisions();
				}
				return Maps[level];
			}
			GetMapResult result = AgbSocket.Instance.GetMap(Character, level, 15000);
			if (result == null)
			{
				return null;
			}
			if (result.Result != 0)
			{
				throw new ArgumentException("Failed getting map: " + result.Result);
			}
			if (result.Map == null)
			{
				return null;
			}
			Maps.Add(level, result.Map);
			return result.Map;
		}
	}
}
