using System;
using D2Data;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class GetMap : AGBPacket
{
	public Character Character;

	public AreaLevel AreaLevel;

	public override PacketType Type => PacketType.GetMap;

	public override byte[] Data
	{
		get
		{
			DataBuffer dataBuffer = new DataBuffer();
			dataBuffer.InsertInt16((short)AreaLevel);
			dataBuffer.InsertByte((byte)Character.Class);
			dataBuffer.InsertByte((byte)Character.Realm);
			dataBuffer.InsertCString(Character.Name);
			return dataBuffer.GetData();
		}
	}

	public GetMap(Character character, AreaLevel areaLevel)
	{
		Character = character;
		AreaLevel = areaLevel;
	}

	public static GetMap Parse(byte[] packetData, int offset)
	{
		if (packetData.Length - offset == 0)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		DataReader reader = new DataReader(data);
		reader.Seek(0);
		AreaLevel areaLevel = (AreaLevel)reader.ReadInt16();
		CharacterClass charClass = (CharacterClass)reader.ReadByte();
		Realm charRealm = (Realm)reader.ReadByte();
		string charName = reader.ReadCString();
		Character character = new Character(charName, charRealm, charClass);
		return new GetMap(character, areaLevel);
	}
}
