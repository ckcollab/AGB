using D2Data;

namespace AGB.D2.Net.D2.RC;

public class CreateGame : BasePacket
{
	private readonly ushort RequestId;

	private readonly uint DifficultyType;

	private readonly byte Unknown = 1;

	private readonly byte PlayerDiff = byte.MaxValue;

	private readonly byte MaxPlayers = 8;

	private readonly string GameName;

	private readonly string GamePassword;

	private readonly string GameDescription;

	public byte[] Data;

	public CreateGame(ushort GameNumber, GameDifficulty difficulty, string gamename, string gamepsw, string gamedesc)
		: base(3)
	{
		uint diff = 0u;
		switch (difficulty)
		{
		case GameDifficulty.Normal:
			diff = 0u;
			break;
		case GameDifficulty.Nightmare:
			diff = 4096u;
			break;
		case GameDifficulty.Hell:
			diff = 8192u;
			break;
		}
		RequestId = GameNumber;
		DifficultyType = diff;
		GameName = gamename;
		GamePassword = gamepsw;
		GameDescription = gamedesc;
		Data = GetData();
	}

	public override byte[] GetData()
	{
		InsertUInt16(RequestId);
		InsertUInt32(DifficultyType);
		InsertByte(Unknown);
		InsertByte(PlayerDiff);
		InsertByte(MaxPlayers);
		InsertCString(GameName);
		InsertCString(GamePassword);
		InsertCString(GameDescription);
		return base.GetData();
	}
}
