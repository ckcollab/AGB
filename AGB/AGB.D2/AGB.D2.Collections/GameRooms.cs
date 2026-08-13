using System;
using AGB.Collections;
using D2Data;

namespace AGB.D2.Collections;

public class GameRooms : ThreadSafeList<GameRoom>
{
	public AreaLevel GetCurrentAreaLevel(int X, int Y)
	{
		lock (PadLock)
		{
			foreach (GameRoom gameroom in ItemList)
			{
				if (gameroom.X <= X && gameroom.X + 40 >= X && gameroom.Y <= Y && gameroom.Y + 40 >= Y)
				{
					return gameroom.areaLevel;
				}
			}
		}
		return AreaLevel.None;
	}

	private void List()
	{
		foreach (GameRoom room in ItemList)
		{
			Console.WriteLine("\nRooms " + room.X + " " + room.Y + " " + room.areaLevel);
		}
	}

	public void RemoveRoom(int X, int Y, AreaLevel areaLevel)
	{
		lock (PadLock)
		{
			for (int i = ItemList.Count - 1; i > 0; i--)
			{
				if (ItemList[i].X == X && ItemList[i].Y == Y && areaLevel == ItemList[i].areaLevel)
				{
					Remove(ItemList[i]);
				}
			}
		}
	}
}
