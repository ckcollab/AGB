using D2Data;

namespace AGB.Mapping;

public struct PresetUnit
{
	public int nTxtFileNo;

	public unsafe fixed uint _1[2];

	public int X;

	public unsafe fixed uint _2[1];

	public int Y;

	public unsafe PresetUnit* Next;

	public UnitType Type;
}
