using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class EnterChatRequest
{
	private Character Character;

	public byte[] Data;

	public EnterChatRequest(Character character)
	{
		Character = character;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(10);
		myPacket.InsertCString(Character.Name);
		myPacket.InsertCString(Character.Realm.ToString() + "," + Character.Name);
		return myPacket.GetData();
	}
}
