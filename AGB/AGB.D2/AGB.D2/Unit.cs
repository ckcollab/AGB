namespace AGB.D2;

public class Unit
{
	public Game Game;

	public uint Uid;

	public int X;

	public int Y;

	public Unit(Game game)
	{
		Game = game;
	}

	public virtual void Clear()
	{
		Uid = 0u;
		X = 0;
		Y = 0;
	}
}
