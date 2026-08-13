using System;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class LoginResult : AGBPacket
{
	public LoginResultValue Result;

	public override PacketType Type => PacketType.LoginResult;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertByte((byte)Result);
			return buffer.GetData();
		}
	}

	public static LoginResult Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		LoginResult result = new LoginResult();
		DataReader reader = new DataReader(data);
		result.Result = (LoginResultValue)reader.ReadByte();
		return result;
	}
}
