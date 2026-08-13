namespace AGB.Mapping;

public struct Coll
{
	public int dwPosGameX;

	public int dwPosGameY;

	public int dwSizeGameX;

	public int dwSizeGameY;

	public int dwPosRoomX;

	public int dwPosRoomY;

	public int dwSizeRoomX;

	public int dwSizeRoomY;

	public unsafe ushort* pMapStart;

	public unsafe ushort* pMapEnd;
}
