using D2Data;

namespace AGB.D2.Collections;

public class GameRoom
{
	public int X;

	public int Y;

	public AreaLevel areaLevel;

	public GameRoom(int x, int y, AreaLevel arealevel)
	{
		X = x;
		Y = y;
		areaLevel = arealevel;
	}
}
