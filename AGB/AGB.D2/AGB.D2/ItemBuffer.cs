using System.Drawing;

namespace AGB.D2;

public class ItemBuffer
{
	public int Width;

	public int Height;

	public bool[,] Space;

	public ItemBuffer(int width, int height)
	{
		Width = width;
		Height = height;
		Space = new bool[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Space[x, y] = true;
			}
		}
	}

	public bool HasSpaceFor(Item item)
	{
		return FindSpaceFor(item).X != -1;
	}

	public Point FindSpaceFor(Item item)
	{
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				if (Space[x, y] && IsSpaceAvailable(x, y, item.Action.BaseItem.InvWidth, item.Action.BaseItem.InvHeight))
				{
					return new Point(x, y);
				}
			}
		}
		return new Point(-1, -1);
	}

	private bool IsSpaceAvailable(int startX, int startY, int width, int height)
	{
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (!Space[x + startX, y + startY])
				{
					return false;
				}
			}
		}
		return true;
	}

	public void AddItem(Item item)
	{
		for (int y = 0; y < item.Action.BaseItem.InvHeight; y++)
		{
			for (int x = 0; x < item.Action.BaseItem.InvWidth; x++)
			{
				Space[x + item.X, y + item.Y] = false;
			}
		}
	}

	public void RemoveItem(Item item)
	{
		for (int y = 0; y < item.Action.BaseItem.InvHeight; y++)
		{
			for (int x = 0; x < item.Action.BaseItem.InvWidth; x++)
			{
				Space[x + item.X, y + item.Y] = true;
			}
		}
	}

	public override string ToString()
	{
		string s = "";
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				s += ((!Space[x, y]) ? "#" : "-");
			}
		}
		return s;
	}
}
