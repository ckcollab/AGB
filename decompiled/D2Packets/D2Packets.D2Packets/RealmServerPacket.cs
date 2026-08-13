namespace D2Packets.D2Packets;

public enum RealmServerPacket
{
	RealmStartupResponse = 1,
	CharacterCreationResponse = 2,
	CreateGameResponse = 3,
	JoinGameResponse = 4,
	GameList = 5,
	GameInfo = 6,
	CharacterLogonResponse = 7,
	CharacterDeletionResponse = 10,
	MessageOfTheDay = 18,
	GameCreationQueue = 20,
	CharacterUpgradeResponse = 24,
	CharacterList = 25,
	Invalid = 32
}
