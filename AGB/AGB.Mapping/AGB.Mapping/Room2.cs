using System;

namespace AGB.Mapping;

public struct Room2
{
	public unsafe Level* Level;

	public uint _1;

	public uint dwRoomsNear;

	public IntPtr pRoomTiles;

	public unsafe Room2* pRoom2Near;

	public unsafe fixed uint _3[6];

	public int X;

	public int Y;

	public int Width;

	public int Height;

	public unsafe Room2Info* Type2Info;

	public unsafe fixed uint _4[32];

	public int PresetType;

	public unsafe PresetUnit* PresetUnit;

	public unsafe fixed uint _5[3];

	public unsafe Room2* Next;

	public unsafe Room1* Room1;
}
