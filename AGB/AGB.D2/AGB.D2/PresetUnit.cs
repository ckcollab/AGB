using D2Data;

namespace AGB.D2;

public class PresetUnit
{
	public ushort X;

	public ushort Y;

	public int OffsetX;

	public int OffsetY;

	public ushort Id;

	public UnitType Type;

	public override string ToString()
	{
		return string.Concat("Type = ", Type, "; Id = ", Id);
	}
}
