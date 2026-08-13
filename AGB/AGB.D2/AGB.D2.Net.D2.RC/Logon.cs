using MBNCSUtil;

namespace AGB.D2.Net.D2.RC;

public class Logon
{
	private readonly uint Cookie;

	private readonly uint Status;

	private readonly uint[] MCPChunk1;

	private readonly uint[] MCPChunk2;

	private readonly string BNCSUniqueName;

	public byte[] Data;

	public Logon(uint cookie, uint status, uint[] mcpChunk1, uint[] mcpChunk2, string bncsUniqueName)
	{
		Cookie = cookie;
		Status = status;
		MCPChunk1 = mcpChunk1;
		MCPChunk2 = mcpChunk2;
		BNCSUniqueName = bncsUniqueName;
		Data = Build();
	}

	private byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(1);
		myPacket.InsertUInt32(Cookie);
		myPacket.InsertUInt32(Status);
		myPacket.InsertUInt32Array(MCPChunk1);
		myPacket.InsertUInt32Array(MCPChunk2);
		myPacket.InsertCString(BNCSUniqueName);
		byte[] temp = myPacket.GetData();
		byte[] retVal = new byte[temp.Length - 1];
		retVal[0] = (byte)retVal.Length;
		retVal[1] = 0;
		retVal[2] = 1;
		for (int i = 3; i < retVal.Length; i++)
		{
			retVal[i] = temp[i + 1];
		}
		return retVal;
	}
}
