using System;
using System.Threading;
using AGB.Collections;
using D2Data;
using D2Packets.GameServer;

namespace AGB.D2.Collections;

public class Objects : ThreadSafeList<Object>
{
	private Game Game;

	public Objects(Game game)
	{
		Game = game;
	}

	public void Update(AssignGameObject assignObj)
	{
		lock (PadLock)
		{
			Object obj = ItemList.Find((Object o) => o.Uid == assignObj.UID);
			if (obj == null)
			{
				obj = new Object(Game);
				ItemList.Add(obj);
			}
			obj.Id = assignObj.ObjectID;
			obj.Uid = assignObj.UID;
			obj.X = assignObj.X;
			obj.Y = assignObj.Y;
			obj.State = assignObj.ObjectMode;
			obj.InteractType = assignObj.InteractType;
			obj.Destination = assignObj.Destination;
		}
	}

	public Object Find(GameObjectClass id, int timeOut)
	{
		DateTime start = DateTime.Now;
		while (DateTime.Now.Subtract(start).TotalMilliseconds < (double)timeOut)
		{
			Object o = Find(id);
			if (o != null)
			{
				return o;
			}
			Thread.Sleep(100);
		}
		return null;
	}

	public Object Find(GameObjectClass id)
	{
		return ItemList.Find((Object o) => o.Id == id);
	}

	public void Remove(uint uid)
	{
		lock (PadLock)
		{
			Object obj = ItemList.Find((Object o) => o.Uid == uid);
			if (obj != null)
			{
				ItemList.Remove(obj);
			}
		}
	}
}
