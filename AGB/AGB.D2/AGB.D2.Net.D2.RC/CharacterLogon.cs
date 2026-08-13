namespace AGB.D2.Net.D2.RC;

public class CharacterLogon : BasePacket
{
	private readonly string charname;

	public byte[] Data;

	public CharacterLogon(string charactername)
		: base(7)
	{
		charname = charactername;
		Data = GetData();
	}

	public override byte[] GetData()
	{
		InsertCString(charname);
		return base.GetData();
	}
}
