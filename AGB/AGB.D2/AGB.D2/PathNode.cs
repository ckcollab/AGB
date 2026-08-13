using System;

namespace AGB.D2;

public class PathNode : IComparable
{
	public int X;

	public int Y;

	public PathNode Parent;

	public bool IsOpen = false;

	public float Score;

	public PathNode()
	{
		Score = float.MaxValue;
	}

	public PathNode(int x, int y)
	{
		X = x;
		Y = y;
	}

	public PathNode(int x, int y, int goalX, int goalY)
		: this(x, y)
	{
		Score = GetDistanceFrom(goalX, goalY);
	}

	public int CompareTo(object obj)
	{
		if (Score < (obj as PathNode).Score)
		{
			return -1;
		}
		if (Score == (obj as PathNode).Score)
		{
			return 0;
		}
		return 1;
	}

	public override string ToString()
	{
		return "X = " + X + "; Y = " + Y;
	}

	public float GetDistanceFrom(PathNode node)
	{
		return GetDistanceFrom(node.X, node.Y);
	}

	public float GetDistanceFrom(int x2, int y2)
	{
		return (float)Math.Sqrt((X - x2) * (X - x2) + (Y - y2) * (Y - y2));
	}

	public PathNode GetFarthestNodeVisible(Map map)
	{
		if (Parent == null)
		{
			return null;
		}
		PathNode currentPoint = Parent;
		while (currentPoint.Parent != null)
		{
			if (!HasLineOfSight(map, currentPoint.Parent))
			{
				return currentPoint;
			}
			currentPoint = currentPoint.Parent;
		}
		return null;
	}

	public bool HasLineOfSight(Map map, PathNode p1)
	{
		PathNode p2 = this;
		bool steep = Math.Abs(p1.Y - p2.Y) > Math.Abs(p1.X - p2.X);
		if (steep)
		{
			PathNode tmpPoint = new PathNode(X, Y);
			p2 = new PathNode(tmpPoint.Y, tmpPoint.X);
			tmpPoint = p1;
			p1 = new PathNode(tmpPoint.Y, tmpPoint.X);
		}
		int deltaX = Math.Abs(p1.X - p2.X);
		int deltaY = Math.Abs(p1.Y - p2.Y);
		int error = 0;
		int deltaError = deltaY;
		int yStep = 0;
		int xStep = 0;
		int y = p2.Y;
		int x = p2.X;
		yStep = ((p2.Y < p1.Y) ? 1 : (-1));
		xStep = ((p2.X < p1.X) ? 1 : (-1));
		int tmpX = 0;
		int tmpY = 0;
		while (x != p1.X)
		{
			x += xStep;
			error += deltaError;
			if (2 * error > deltaX)
			{
				y += yStep;
				error -= deltaX;
			}
			if (steep)
			{
				tmpX = y;
				tmpY = x;
			}
			else
			{
				tmpX = x;
				tmpY = y;
			}
			if (!map.IsWalkable(tmpX, tmpY))
			{
				return false;
			}
		}
		return true;
	}

	public override bool Equals(object obj)
	{
		return (obj as PathNode).X == X && (obj as PathNode).Y == Y;
	}
}
