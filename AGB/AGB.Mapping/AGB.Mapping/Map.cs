using System;
using System.Collections.Generic;
using AGB.D2;
using D2Data;

namespace AGB.Mapping;

public class Map : IDisposable
{
	public AreaLevel AreaLevel;

	public ushort X;

	public ushort Y;

	public ushort Width;

	public ushort Height;

	public Room[,] Rooms;

	public byte[,] Collisions;

	public unsafe Map(Level* level)
	{
		AreaLevel = level->nLevelNo;
		X = (ushort)(level->X * 5);
		Y = (ushort)(level->Y * 5);
		Width = (ushort)(level->Width * 5);
		Height = (ushort)(level->Height * 5);
		Collisions = new byte[Width, Height];
		Rooms = new Room[Width, Height];
		if (level->Room2 == null)
		{
			return;
		}
		for (Room2* room2 = level->Room2; room2 != null; room2 = room2->Next)
		{
			Room room = new Room(level, room2);
			Rooms[room.RelativeX, room.RelativeY] = room;
			if (room.PresetType != 2 || MapCache.Instance.Rooms[room.Ds1Def, room.FileNumber] == null)
			{
				ushort* data = room2->Room1->Coll->pMapStart;
				int endY = room.RelativeY + room.Height;
				int endX = room.RelativeX + room.Width;
				for (int y = room.RelativeY; y < endY; y++)
				{
					for (int x = room.RelativeX; x < endX; x++)
					{
						if ((*data & 1) != 1)
						{
							Collisions[x, y] = 1;
						}
						data++;
					}
				}
			}
		}
	}

	public byte[,] GetCollisions(int x, int y, int width, int height)
	{
		byte[,] collisions = new byte[width, height];
		int endY = y + height;
		int endX = x + width;
		int ys = y;
		int yd = 0;
		for (; ys < endY; ys++)
		{
			int xs = x;
			int xd = 0;
			for (; xs < endX; xs++)
			{
				collisions[xd, yd] = Collisions[xs, ys];
				xd++;
			}
			yd++;
		}
		return collisions;
	}

	public List<Room> GetDs1Rooms()
	{
		List<Room> ds1Starts = new List<Room>();
		for (int x = 0; x < Width; x += 2)
		{
			for (int y = 0; y < Height; y += 2)
			{
				Room room = Rooms[x, y];
				if (room == null || room.PresetType != 2 || room.IsPartOfADs1)
				{
					continue;
				}
				ds1Starts.Add(room);
				int ds1EndY = room.RelativeY + room.Ds1.Height * 5;
				int ds1EndX = room.RelativeX + room.Ds1.Width * 5;
				for (int otherRoomY = y; otherRoomY < ds1EndY; otherRoomY += 2)
				{
					for (int otherRoomX = x; otherRoomX < ds1EndX; otherRoomX += 2)
					{
						Room otherRoom = Rooms[otherRoomX, otherRoomY];
						if (otherRoom == null || room.Ds1Def != otherRoom.Ds1Def || room.FileNumber != otherRoom.FileNumber)
						{
							continue;
						}
						foreach (AGB.D2.PresetUnit p in otherRoom.PresetUnits)
						{
							p.X += (ushort)(otherRoom.RelativeX - room.RelativeX);
							p.Y += (ushort)(otherRoom.RelativeY - room.RelativeY);
						}
						room.PresetUnits.AddRange(otherRoom.PresetUnits);
						otherRoom.IsPartOfADs1 = true;
					}
				}
			}
		}
		return ds1Starts;
	}

	public List<Room> GetCachedDs1Rooms()
	{
		List<Room> ds1Starts = new List<Room>();
		for (int x = 0; x < Width; x += 2)
		{
			for (int y = 0; y < Height; y += 2)
			{
				Room room = Rooms[x, y];
				if (room == null || room.IsPartOfADs1 || room.Ds1Def >= 1090 || MapCache.Instance.Rooms[room.Ds1Def, room.FileNumber] == null)
				{
					continue;
				}
				ds1Starts.Add(room);
				int ds1EndY = room.RelativeY + room.Ds1.Height * 5;
				int ds1EndX = room.RelativeX + room.Ds1.Width * 5;
				for (int otherRoomY = y; otherRoomY < ds1EndY; otherRoomY += 2)
				{
					for (int otherRoomX = x; otherRoomX < ds1EndX; otherRoomX += 2)
					{
						Room otherRoom = Rooms[otherRoomX, otherRoomY];
						if (otherRoom != null && room.Ds1Def == otherRoom.Ds1Def && room.FileNumber == otherRoom.FileNumber)
						{
							otherRoom.IsPartOfADs1 = true;
						}
					}
				}
			}
		}
		return ds1Starts;
	}

	public List<Room> GetNonDs1Rooms()
	{
		List<Room> nonDs1Rooms = new List<Room>();
		for (int x = 0; x < Width; x += 2)
		{
			for (int y = 0; y < Height; y += 2)
			{
				if (Rooms[x, y] != null && !Rooms[x, y].IsPartOfADs1)
				{
					nonDs1Rooms.Add(Rooms[x, y]);
				}
			}
		}
		return nonDs1Rooms;
	}

	public List<AGB.D2.Room> GetAllRooms()
	{
		List<AGB.D2.Room> rooms = new List<AGB.D2.Room>();
		foreach (Room room2 in GetCachedDs1Rooms())
		{
			rooms.Add(new AGB.D2.Room
			{
				Id1 = room2.Ds1Def,
				Id2 = room2.FileNumber,
				X = room2.RelativeX,
				Y = room2.RelativeY
			});
		}
		foreach (Room room in GetNonDs1Rooms())
		{
			rooms.Add(new AGB.D2.Room
			{
				Collisions = GetCollisions(room.RelativeX, room.RelativeY, room.Width, room.Height),
				X = room.RelativeX,
				Y = room.RelativeY,
				PresetUnits = room.PresetUnits.ToArray()
			});
		}
		return rooms;
	}

	public void Dispose()
	{
		Collisions = null;
		Room[,] rooms = Rooms;
		int upperBound = rooms.GetUpperBound(0);
		int upperBound2 = rooms.GetUpperBound(1);
		for (int i = rooms.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = rooms.GetLowerBound(1); j <= upperBound2; j++)
			{
				rooms[i, j]?.Dispose();
			}
		}
		Rooms = null;
	}
}
