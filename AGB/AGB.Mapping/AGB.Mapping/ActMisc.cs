namespace AGB.Mapping;

public struct ActMisc
{
	public uint _1;

	public unsafe Act* ptrAct;

	public unsafe fixed byte _2[1128];

	public unsafe Level* ptrLevelFirst;
}
