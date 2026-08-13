using System;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class WelcomeResult : AGBPacket
{
	public string Message;

	public override PacketType Type => PacketType.WelcomeResult;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertCString(Message);
			return buffer.GetData();
		}
	}

	public static WelcomeResult Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		WelcomeResult welcomeResult = new WelcomeResult();
		DataReader reader = new DataReader(data);
		welcomeResult.Message = reader.ReadCString();
		return welcomeResult;
	}
}
