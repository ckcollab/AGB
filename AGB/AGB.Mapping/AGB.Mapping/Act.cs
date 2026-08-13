namespace AGB.Mapping;

public struct Act
{
	public unsafe fixed byte _1[52];

	public unsafe Room1* pRoom1;

	public unsafe ActMisc* Misc;

	public unsafe fixed uint _2[2];

	public uint dwAct;

	public uint pfnCallBack;
}
