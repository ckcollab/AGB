using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class SetEmail
{
	private string Email;

	public byte[] Data;

	public SetEmail(string email)
	{
		Email = email;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(89);
		if (Email != null)
		{
			myPacket.InsertCString(Email);
		}
		else
		{
			myPacket.InsertByte(0);
		}
		return myPacket.GetData();
	}
}
