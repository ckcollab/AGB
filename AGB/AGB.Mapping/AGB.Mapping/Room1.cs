using System;

namespace AGB.Mapping;

public struct Room1
{
	public unsafe Room1** RoomsNear;

	public unsafe fixed uint _1[2];

	public unsafe fixed uint dwSeed[2];

	public uint _2;

	public int X;

	public int Y;

	public int Width;

	public int Height;

	public unsafe fixed uint _3[4];

	public unsafe Room1* Next;

	public uint _4;

	public IntPtr pUnitFirst;

	public unsafe fixed uint _5[3];

	public unsafe Coll* Coll;

	public unsafe fixed uint _6[7];

	public unsafe Room2* Room2;

	public uint _7;

	public uint dwRoomsNear;
}
