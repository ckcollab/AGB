using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AGB.D2;
using D2Data;

namespace AGB.Mapping;

public class MapServer
{
	private static MapServer instance = null;

	private static readonly object padlock = new object();

	private object MapLock = new object();

	public static MapServer Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = new MapServer();
				}
				return instance;
			}
		}
	}

	[DllImport("map_eng.dll")]
	private static extern int me_init();

	[DllImport("map_eng.dll")]
	private unsafe static extern Act* me_load_act(int act, int seed, int levelid, int difficulty);

	[DllImport("map_eng.dll")]
	private static extern int me_unload_act(IntPtr actPointer);

	[DllImport("map_eng.dll")]
	private static extern int me_load_rooms(IntPtr actPointer, int levelid);

	[DllImport("map_eng.dll")]
	private static extern int me_load_room(IntPtr actPointer, int levelid, int x, int y, int unknown);

	private MapServer()
	{
	}

	public void Init()
	{
		me_init();
	}

	public static int AreaLevelToAct(AreaLevel areaLevel)
	{
		int actNumber = 0;
		if (areaLevel <= AreaLevel.MooMooFarm)
		{
			return 0;
		}
		if (areaLevel <= AreaLevel.ArcaneSanctuary)
		{
			return 1;
		}
		if (areaLevel <= AreaLevel.DuranceOfHateLevel3)
		{
			return 2;
		}
		if (areaLevel <= AreaLevel.ChaosSanctuary)
		{
			return 3;
		}
		return 4;
	}

	public List<Map> GetCollisionMaps(List<AreaLevel> areaLevels, int seed, GameDifficulty difficulty)
	{
		List<Map> maps = new List<Map>();
		foreach (AreaLevel areaLevel in areaLevels)
		{
			maps.Add(GetCollisionMap(areaLevel, seed, difficulty));
		}
		return maps;
	}

	public AGB.D2.Map GetNormalMap(AreaLevel areaLevel, int seed, GameDifficulty difficulty)
	{
		Map map = Instance.GetCollisionMap(areaLevel, seed, difficulty);
		AGB.D2.Map tempMap = new AGB.D2.Map(map.AreaLevel, map.GetAllRooms(), map.X, map.Y, map.Width, map.Height);
		tempMap.LoadCollisions();
		return tempMap;
	}

	public unsafe Map GetCollisionMap(AreaLevel areaLevel, int seed, GameDifficulty difficulty)
	{
		Map map = null;
		lock (MapLock)
		{
			int actNumber = AreaLevelToAct(areaLevel);
			Act* act = me_load_act(actNumber, seed, (int)areaLevel, (int)difficulty);
			for (Level* level = act->Misc->ptrLevelFirst; level != null; level = level->Next)
			{
				if (level->nLevelNo == areaLevel)
				{
					for (Room2* room2 = level->Room2; room2 != null; room2 = room2->Next)
					{
						if (room2->PresetType != 2 || MapCache.Instance.Rooms[room2->Type2Info->PresetDs1Info->Ds1Def, room2->Type2Info->PresetDs1Info->FileNumber] == null)
						{
							me_load_room((IntPtr)act, (int)areaLevel, room2->X, room2->Y, 0);
						}
					}
					map = new Map(level);
					break;
				}
			}
			me_unload_act((IntPtr)act);
			return map;
		}
	}

	public unsafe List<Map> GetSimpleAct(AreaLevel areaLevel, int seed, GameDifficulty difficulty)
	{
		int actNumber = AreaLevelToAct(areaLevel);
		List<Map> maps = new List<Map>();
		lock (MapLock)
		{
			Act* act = me_load_act(actNumber, seed, (int)areaLevel, (int)difficulty);
			for (Level* level = act->Misc->ptrLevelFirst; level != null; level = level->Next)
			{
				maps.Add(new Map(level));
			}
			me_unload_act((IntPtr)act);
			return maps;
		}
	}

	public unsafe Map GetSimpleMap(AreaLevel areaLevel, int seed, GameDifficulty difficulty)
	{
		int actNumber = AreaLevelToAct(areaLevel);
		lock (MapLock)
		{
			Act* act = me_load_act(actNumber, seed, (int)areaLevel, (int)difficulty);
			Level* level = act->Misc->ptrLevelFirst;
			while (level->nLevelNo != areaLevel)
			{
				level = level->Next;
			}
			Map map = new Map(level);
			me_unload_act((IntPtr)act);
			return map;
		}
	}
}
