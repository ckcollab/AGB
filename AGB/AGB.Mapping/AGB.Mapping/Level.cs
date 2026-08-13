using D2Data;

namespace AGB.Mapping;

public struct Level
{
	public unsafe fixed byte _1[80];

	public unsafe fixed uint Seed[2];

	public uint _2;

	public unsafe Level* Next;

	public unsafe fixed byte _3[12];

	public int X;

	public int Y;

	public int Width;

	public int Height;

	public unsafe fixed uint _4[6];

	public AreaLevel nLevelNo;

	public unsafe fixed uint _5[97];

	public unsafe Room2* Room2;
}
