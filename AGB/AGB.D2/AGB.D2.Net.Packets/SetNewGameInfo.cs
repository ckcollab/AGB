using System;
using D2Data;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class SetNewGameInfo : AGBPacket
{
	public Character Character;

	public int Seed;

	public int GameHash;

	public GameDifficulty Difficulty;

	public override PacketType Type => PacketType.SetNewGameInfo;

	public override byte[] Data
	{
		get
		{
			DataBuffer buffer = new DataBuffer();
			buffer.InsertCString(Character.Name);
			buffer.InsertByte((byte)Character.Class);
			buffer.InsertByte((byte)Character.Realm);
			buffer.InsertInt32(Seed);
			buffer.InsertInt32(GameHash);
			buffer.InsertByte((byte)Difficulty);
			return buffer.GetData();
		}
	}

	public SetNewGameInfo(Character character, int seed, int gameHash, GameDifficulty difficulty)
	{
		Character = character;
		Seed = seed;
		GameHash = gameHash;
		Difficulty = difficulty;
	}

	public static SetNewGameInfo Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		DataReader reader = new DataReader(data);
		Character character = new Character();
		character.Name = reader.ReadCString();
		character.Class = (CharacterClass)reader.ReadByte();
		character.Realm = (Realm)reader.ReadByte();
		int seed = reader.ReadInt32();
		int gameHash = reader.ReadInt32();
		GameDifficulty difficulty = (GameDifficulty)reader.ReadByte();
		return new SetNewGameInfo(character, seed, gameHash, difficulty);
	}
}
