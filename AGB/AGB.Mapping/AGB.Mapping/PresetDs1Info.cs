using System.Runtime.InteropServices;

namespace AGB.Mapping;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct PresetDs1Info
{
	public int Ds1Def;

	public int FileNumber;
}
