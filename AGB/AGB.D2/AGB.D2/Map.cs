using System;
using System.Collections.Generic;
using System.Drawing;
using AGB.Collections;
using D2Data;

namespace AGB.D2;

public class Map
{
	public List<Room> Rooms;

	public AreaLevel AreaLevel;

	public ushort Width;

	public ushort Height;

	public ushort X;

	public ushort Y;

	public int StitchedX;

	public int StitchedY;

	public int StitchedWidth;

	public int StitchedHeight;

	public byte[,] Collisions;

	public List<PresetUnit> PresetUnits = new List<PresetUnit>();

	public List<Map> StitchedMaps;

	public Map(AreaLevel areaLevel, List<Room> rooms, ushort x, ushort y, ushort width, ushort height)
	{
		AreaLevel = areaLevel;
		Rooms = rooms;
		StitchedMaps = new List<Map>();
		StitchedMaps.Add(this);
		X = x;
		Y = y;
		Width = width;
		Height = height;
		StitchedX = x;
		StitchedY = y;
		StitchedWidth = width;
		StitchedHeight = height;
	}

	public void LoadCollisions()
	{
		if (Collisions != null)
		{
			return;
		}
		Collisions = new byte[Width, Height];
		foreach (Room room in Rooms)
		{
			if (room.Id1 != 0 && MapCache.Instance.Rooms[room.Id1, room.Id2] != null)
			{
				room.Collisions = MapCache.Instance.Rooms[room.Id1, room.Id2].Collisions;
				room.PresetUnits = MapCache.Instance.Rooms[room.Id1, room.Id2].PresetUnits;
			}
			int endY = room.Collisions.GetUpperBound(1) + 1;
			int endX = room.Collisions.GetUpperBound(0) + 1;
			int ys = 0;
			int yd = room.Y;
			for (; ys < endY; ys++)
			{
				int xs = 0;
				int xd = room.X;
				for (; xs < endX; xs++)
				{
					Collisions[xd, yd] = room.Collisions[xs, ys];
					xd++;
				}
				yd++;
			}
			PresetUnit[] presetUnits = room.PresetUnits;
			foreach (PresetUnit unit in presetUnits)
			{
				PresetUnit offsetUnit = new PresetUnit();
				offsetUnit.Id = unit.Id;
				offsetUnit.Type = unit.Type;
				offsetUnit.X = (ushort)(unit.X + room.X + X);
				offsetUnit.Y = (ushort)(unit.Y + room.Y + Y);
				if (offsetUnit.Type == UnitType.GameObject && (offsetUnit.Id == 37 || offsetUnit.Id == 434 || offsetUnit.Id == 437 || offsetUnit.Id == 482 || offsetUnit.Id == 436 || offsetUnit.Id == 437 || offsetUnit.Id == 48 || offsetUnit.Id == 49 || offsetUnit.Id == 370))
				{
					AddCollisionBlock(offsetUnit.X - X, offsetUnit.Y - Y, 2, 2);
				}
				PresetUnits.Add(offsetUnit);
			}
		}
	}

	public void AddCollisionBlock(int x, int y, int width, int height)
	{
		int endY = y + height;
		int endX = x + width;
		for (int yD = y; yD < endY; yD++)
		{
			for (int xD = x; xD < endX; xD++)
			{
				if (yD > 0 && yD < Height && xD > 0 && xD < Width)
				{
					Collisions[xD, yD] = 0;
				}
			}
		}
	}

	private byte[,] ThickWalls()
	{
		byte[,] tempCollisions = new byte[Width, Height];
		int endY = Height - 1;
		int endX = Width - 1;
		for (int y = 1; y < endY; y++)
		{
			for (int x = 1; x < endX; x++)
			{
				tempCollisions[x, y] = Collisions[x, y];
			}
		}
		for (int y = 1; y < endY; y++)
		{
			for (int x = 1; x < endX; x++)
			{
				if (Collisions[x, y] == 0)
				{
					tempCollisions[x + 1, y + 1] = 0;
					tempCollisions[x + 1, y] = 0;
					tempCollisions[x + 1, y - 1] = 0;
					tempCollisions[x, y + 1] = 0;
					tempCollisions[x, y - 1] = 0;
					tempCollisions[x - 1, y + 1] = 0;
					tempCollisions[x - 1, y] = 0;
					tempCollisions[x - 1, y - 1] = 0;
				}
			}
		}
		return tempCollisions;
	}

	public void FillOutside()
	{
		for (int y = 0; y < Height; y++)
		{
			Collisions[0, y] = 0;
		}
		for (int y = 0; y < Height; y++)
		{
			Collisions[Width - 1, y] = 0;
		}
		for (int x = 0; x < Width; x++)
		{
			Collisions[x, 0] = 0;
		}
		for (int x = 0; x < Width; x++)
		{
			Collisions[x, Height - 1] = 0;
		}
		Queue<PathNode> nodes = new Queue<PathNode>();
		nodes.Enqueue(new PathNode(Width - 1, Height - 1));
		while (nodes.Count > 0)
		{
			PathNode node = nodes.Dequeue();
			if (Collisions[node.X, node.Y] == 0)
			{
				Collisions[node.X, node.Y] = 1;
				if (node.X - 1 >= 0 && Collisions[node.X - 1, node.Y] == 0)
				{
					nodes.Enqueue(new PathNode(node.X - 1, node.Y));
				}
				if (node.X + 1 < Width && Collisions[node.X + 1, node.Y] == 0)
				{
					nodes.Enqueue(new PathNode(node.X + 1, node.Y));
				}
				if (node.Y + 1 < Height && Collisions[node.X, node.Y + 1] == 0)
				{
					nodes.Enqueue(new PathNode(node.X, node.Y + 1));
				}
				if (node.Y - 1 >= 0 && Collisions[node.X, node.Y - 1] == 0)
				{
					nodes.Enqueue(new PathNode(node.X, node.Y - 1));
				}
			}
		}
	}

	public PresetUnit FindPresetUnit(UnitType type, int id)
	{
		foreach (Map map in StitchedMaps)
		{
			PresetUnit unit = map.PresetUnits.Find((PresetUnit p) => p.Id == id && p.Type == type);
			if (unit != null)
			{
				return unit;
			}
		}
		return null;
	}

	public PresetUnit FindPresetUnit(PresetUnit[] presetUnits)
	{
		foreach (Map map in StitchedMaps)
		{
			PresetUnit i;
			for (int j = 0; j < presetUnits.Length; j++)
			{
				i = presetUnits[j];
				PresetUnit exit = map.PresetUnits.Find((PresetUnit p) => p.Id == i.Id && p.Type == i.Type);
				if (exit != null)
				{
					return exit;
				}
			}
		}
		return null;
	}

	public PresetUnit FindPresetUnit(AreaLevel level, PresetUnit[] presetUnits)
	{
		foreach (Map map in StitchedMaps)
		{
			if (map.AreaLevel != level)
			{
				continue;
			}
			PresetUnit i;
			for (int j = 0; j < presetUnits.Length; j++)
			{
				i = presetUnits[j];
				PresetUnit exit = map.PresetUnits.Find((PresetUnit p) => p.Id == i.Id && p.Type == i.Type);
				if (exit != null)
				{
					return exit;
				}
			}
		}
		return null;
	}

	public PresetUnit FindWayPoint()
	{
		foreach (PresetUnit unit in PresetUnits)
		{
			if (unit.Type == UnitType.GameObject && (unit.Id == 119 || unit.Id == 157 || unit.Id == 156 || unit.Id == 237 || unit.Id == 398 || unit.Id == 429 || unit.Id == 402 || unit.Id == 323 || unit.Id == 288 || unit.Id == 324 || unit.Id == 238 || unit.Id == 496 || unit.Id == 511 || unit.Id == 494))
			{
				return unit;
			}
		}
		return null;
	}

	public PresetUnit FindWarps(int[] ids)
	{
		foreach (Map map in StitchedMaps)
		{
			foreach (PresetUnit unit in map.PresetUnits)
			{
				for (int i = 0; i < ids.Length; i++)
				{
					if (unit.Type == UnitType.Warp && unit.Id == ids[i])
					{
						return unit;
					}
				}
			}
		}
		return null;
	}

	public PresetUnit FindWarps(int[] ids, AreaLevel level)
	{
		foreach (Map map in StitchedMaps)
		{
			if (map.AreaLevel != level)
			{
				continue;
			}
			foreach (PresetUnit unit in map.PresetUnits)
			{
				for (int i = 0; i < ids.Length; i++)
				{
					if (unit.Type == UnitType.Warp && unit.Id == ids[i])
					{
						return unit;
					}
				}
			}
		}
		return null;
	}

	public bool IsInBounds(int x, int y)
	{
		return x > X && x < X + Width && y > Y && y < Y + Height;
	}

	public void StitchWith(Map otherMap)
	{
		if (otherMap.X == X && otherMap.Y == Y)
		{
			throw new ArgumentException("Can't stitch a map to itself!");
		}
		StitchedMaps.Add(otherMap);
		StitchedX = int.MaxValue;
		foreach (Map map in StitchedMaps)
		{
			if (map.X < StitchedX)
			{
				StitchedX = map.X;
				StitchedY = map.Y;
			}
		}
		StitchedHeight = 0;
		StitchedWidth = 0;
		foreach (Map map in StitchedMaps)
		{
			StitchedWidth += map.Width;
			StitchedHeight += map.Height;
		}
	}

	public List<PathNode> GetTeleportPath(PresetUnit[] starts, int[] warpIds)
	{
		PresetUnit start = FindPresetUnit(starts);
		if (start == null)
		{
			return null;
		}
		PresetUnit exit = FindWarps(warpIds);
		if (exit == null)
		{
			return null;
		}
		return GetTeleportPath(start.X, start.Y, exit.X, exit.Y);
	}

	public List<PathNode> GetTeleportPath(int x, int y, int[] warpIds)
	{
		PresetUnit exit = FindWarps(warpIds);
		if (exit == null)
		{
			return null;
		}
		return GetTeleportPath(x, y, exit.X, exit.Y);
	}

	public List<PathNode> GetTeleportPath(PresetUnit[] starts, PresetUnit[] exits)
	{
		PresetUnit start = FindPresetUnit(starts);
		if (start == null)
		{
			return null;
		}
		PresetUnit exit = FindPresetUnit(exits);
		if (exit == null)
		{
			return null;
		}
		return GetTeleportPath(start.X, start.Y, exit.X, exit.Y);
	}

	public List<PathNode> GetTeleportPath(PresetUnit[] starts, AreaLevel exitArea, int[] exits)
	{
		PresetUnit start = FindPresetUnit(starts);
		if (start == null)
		{
			return null;
		}
		PresetUnit exit = FindWarps(exits, exitArea);
		if (exit == null)
		{
			return null;
		}
		return GetTeleportPath(start.X, start.Y, exit.X, exit.Y);
	}

	public List<PathNode> GetTeleportPath(int x1, int y1, PresetUnit[] exits)
	{
		PresetUnit exit = FindPresetUnit(exits);
		if (exit == null)
		{
			return null;
		}
		return GetTeleportPath(x1, y1, exit.X, exit.Y);
	}

	public List<PathNode> GetTeleportPath(int x1, int y1, int x2, int y2)
	{
		if (!IsInBounds(x1, y1))
		{
			return null;
		}
		List<PathNode> path = new List<PathNode>();
		x1 -= StitchedX;
		y1 -= StitchedY;
		x2 -= StitchedX;
		y2 -= StitchedY;
		int radius = 30;
		PriorityQueueB<PathNode> open = new PriorityQueueB<PathNode>();
		PathNode[,] nodeGrid = new PathNode[StitchedWidth, StitchedHeight];
		PathNode firstNode = new PathNode(x1, y1, x2, y2);
		firstNode.IsOpen = true;
		nodeGrid[x1, y1] = firstNode;
		open.Push(firstNode);
		while (open.Count > 0)
		{
			PathNode node = open.Pop();
			if ((node.X == x2 && node.Y == y2) || node.Score <= 4f)
			{
				while (node.Parent != null)
				{
					if (node.Parent != node.Parent.Parent)
					{
						path.Add(node);
					}
					node = node.Parent;
				}
				if (path.Count == 0)
				{
					path.Add(node);
				}
				break;
			}
			for (int y3 = -radius; y3 < radius; y3 += 2)
			{
				for (int x3 = -radius; x3 < radius; x3 += 2)
				{
					int px = node.X + x3;
					int py = node.Y + y3;
					if (IsWalkable(px, py))
					{
						if (nodeGrid[px, py] == null)
						{
							nodeGrid[px, py] = new PathNode(px, py, x2, y2);
							nodeGrid[px, py].Parent = node;
						}
						if (!nodeGrid[px, py].IsOpen)
						{
							nodeGrid[px, py].IsOpen = true;
							open.Push(nodeGrid[px, py]);
						}
					}
				}
			}
		}
		foreach (PathNode node in path)
		{
			node.X += StitchedX;
			node.Y += StitchedY;
		}
		path.Reverse();
		return path;
	}

	public List<PathNode> GetWalkPath(int x, int y, int[] warpIds)
	{
		PresetUnit exit = FindWarps(warpIds);
		if (exit == null)
		{
			return null;
		}
		return GetWalkPath(x, y, exit.X, exit.Y);
	}

	public List<PathNode> GetWalkPath(int x, int y, PresetUnit unit)
	{
		if (unit == null)
		{
			return null;
		}
		return GetWalkPath(x, y, unit.X, unit.Y);
	}

	public List<PathNode> GetWalkPath(int x1, int y1, PresetUnit[] exits)
	{
		PresetUnit exit = FindPresetUnit(exits);
		if (exit == null)
		{
			return null;
		}
		return GetWalkPath(x1, y1, exit.X, exit.Y);
	}

	public List<PathNode> GetWalkPath(int x1, int y1, int x2, int y2)
	{
		return GetWalkPath(x1, y1, x2, y2, 8);
	}

	public List<PathNode> GetWalkPath(int x1, int y1, int x2, int y2, int pathNodeDistance)
	{
		PriorityQueueB<PathNode> open = new PriorityQueueB<PathNode>();
		PathNode[,] nodeGrid = new PathNode[StitchedWidth, StitchedHeight];
		List<PathNode> path = new List<PathNode>();
		x1 -= StitchedX;
		y1 -= StitchedY;
		x2 -= StitchedX;
		y2 -= StitchedY;
		open.Push(new PathNode(x1, y1, x2, y2));
		while (open.Count > 0)
		{
			PathNode node = open.Pop();
			if ((node.X == x2 && node.Y == y2) || node.Score < 5f)
			{
				path.Add(new PathNode(x2, y2));
				int pathCounter = 0;
				while (node.Parent != null)
				{
					if (pathCounter % pathNodeDistance == 0 && node.Parent != node.Parent.Parent)
					{
						path.Add(node);
					}
					pathCounter++;
					node = node.Parent;
				}
				break;
			}
			for (int y3 = -1; y3 < 2; y3++)
			{
				for (int x3 = -1; x3 < 2; x3++)
				{
					if (x3 == 0 && y3 == 0)
					{
						continue;
					}
					int px = node.X + x3;
					int py = node.Y + y3;
					bool walkable = true;
					for (int scanY = -1; scanY < 2; scanY++)
					{
						for (int scanX = -1; scanX < 2; scanX++)
						{
							if (!IsWalkable(px + scanX, py + scanY))
							{
								walkable = false;
							}
						}
					}
					if (walkable)
					{
						if (nodeGrid[px, py] == null)
						{
							nodeGrid[px, py] = new PathNode(px, py, x2, y2);
							nodeGrid[px, py].Parent = node;
						}
						PathNode newNode = nodeGrid[px, py];
						if (!newNode.IsOpen)
						{
							newNode.IsOpen = true;
							open.Push(newNode);
						}
					}
				}
			}
		}
		foreach (PathNode offsetNode in path)
		{
			offsetNode.X += StitchedX;
			offsetNode.Y += StitchedY;
		}
		path.Reverse();
		return path;
	}

	public bool IsWalkable(int x, int y)
	{
		bool walkable = false;
		foreach (Map map in StitchedMaps)
		{
			int mapX = x - (map.X - StitchedX);
			int mapY = y - (map.Y - StitchedY);
			if (mapX >= map.Width || mapX < 0 || mapY >= map.Height || mapY < 0 || map.Collisions[mapX, mapY] != 1)
			{
				continue;
			}
			walkable = true;
			break;
		}
		return walkable;
	}

	public void DumpMap(string fileName)
	{
		DumpMap(fileName, null);
	}

	public void DumpMap(string fileName, List<PathNode> path)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		int baseX = int.MaxValue;
		int baseY = int.MaxValue;
		int height = 0;
		int width = 0;
		foreach (Map map in StitchedMaps)
		{
			if (map.X < baseX)
			{
				baseX = map.X;
			}
			if (map.Y < baseY)
			{
				baseY = map.Y;
			}
			height += map.Height;
			width += map.Width;
		}
		Bitmap bitmap = new Bitmap(width, height);
		Graphics g = Graphics.FromImage((Image)(object)bitmap);
		g.TranslateTransform(0f, 0f);
		foreach (Map map in StitchedMaps)
		{
			Image image = Util.GetCollisionImage(map.Collisions);
			g.DrawImage(image, new Point(map.X - baseX, map.Y - baseY));
		}
		if (path != null && path.Count != 0)
		{
			PathNode trailingNode = new PathNode(-1, -1);
			foreach (PathNode node in path)
			{
				if (trailingNode.X != -1 && trailingNode.Y != -1)
				{
					g.DrawLine(new Pen(Brushes.get_Red()), new Point(node.X - baseX + 2, node.Y - baseY + 2), new Point(trailingNode.X - baseX + 2, trailingNode.Y - baseY + 4));
				}
				g.DrawRectangle(new Pen(Brushes.get_Red()), node.X - baseX, node.Y - baseY, 4, 4);
				g.FillRectangle(Brushes.get_White(), node.X - baseX + 1, node.Y - baseY + 1, 3, 3);
				trailingNode = node;
			}
		}
		g.Save();
		Image done = (Image)(object)bitmap;
		done.Save(fileName + ".png");
	}
}
