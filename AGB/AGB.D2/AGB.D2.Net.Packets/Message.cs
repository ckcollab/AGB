using System;
using D2Data;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class Message : AGBPacket
{
	public string UserName;

	public Character Sender;

	public Character Receiver;

	public string Body;

	public override PacketType Type => PacketType.Message;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertCString(UserName);
			buffer.InsertCString(Sender.Name);
			buffer.InsertByte((byte)Sender.Class);
			buffer.InsertByte((byte)Sender.Realm);
			buffer.InsertCString(Receiver.Name);
			buffer.InsertByte((byte)Receiver.Class);
			buffer.InsertByte((byte)Receiver.Realm);
			buffer.InsertCString(Body);
			return buffer.GetData();
		}
	}

	public Message(string userName, Character sender, Character receiver, string message)
	{
		UserName = userName;
		Sender = sender;
		Receiver = receiver;
		Body = message;
	}

	public static Message Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		DataReader reader = new DataReader(data);
		Message result = new Message(null, null, null, null);
		result.UserName = reader.ReadCString();
		result.Sender = new Character();
		result.Sender.Name = reader.ReadCString();
		result.Sender.Class = (CharacterClass)reader.ReadByte();
		result.Sender.Realm = (Realm)reader.ReadByte();
		result.Receiver = new Character();
		result.Receiver.Name = reader.ReadCString();
		result.Receiver.Class = (CharacterClass)reader.ReadByte();
		result.Receiver.Realm = (Realm)reader.ReadByte();
		result.Body = reader.ReadCString();
		return result;
	}
}
