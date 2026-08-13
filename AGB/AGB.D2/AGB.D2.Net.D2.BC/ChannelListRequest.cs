using System;
using D2Data;
using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class ChannelListRequest
{
	private BattleNetClient Client;

	public byte[] Data;

	public ChannelListRequest(BattleNetClient client)
	{
		Client = client;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(11);
		myPacket.InsertByteArray(Client switch
		{
			BattleNetClient.Diablo2LoD => new byte[4] { 80, 88, 50, 68 }, 
			BattleNetClient.Diablo2 => new byte[4] { 86, 68, 50, 68 }, 
			_ => throw new NotImplementedException("The client type: '" + Client.ToString() + "' is not implemented."), 
		});
		return myPacket.GetData();
	}
}
