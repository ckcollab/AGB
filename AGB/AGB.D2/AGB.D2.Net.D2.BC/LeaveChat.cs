using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class LeaveChat
{
	public byte[] Data;

	public LeaveChat()
	{
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(16);
		return myPacket.GetData();
	}
}
