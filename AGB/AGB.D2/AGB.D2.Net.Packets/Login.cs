using System;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class Login : AGBPacket
{
	public string Username;

	public string Password;

	public byte Version;

	public static readonly byte CurrentVersion = 5;

	public override PacketType Type => PacketType.Login;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertCString(Username);
			buffer.InsertCString(Password);
			buffer.InsertByte(Version);
			return buffer.GetData();
		}
	}

	public Login(string username, string password)
	{
		Username = username;
		Password = password;
		Version = CurrentVersion;
	}

	public static Login Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		DataReader reader = new DataReader(data);
		string username = reader.ReadCString();
		string password = reader.ReadCString();
		byte version = reader.ReadByte();
		Login result = new Login(username, password);
		result.Version = version;
		return result;
	}
}
