namespace D2Packets.D2Packets;

public enum RealmClientPacket
{
	RealmStartupRequest = 1,
	CharacterCreationRequest = 2,
	CreateGameRequest = 3,
	JoinGameRequest = 4,
	GameListRequest = 5,
	GameInfoRequest = 6,
	CharacterLogonRequest = 7,
	CharacterDeletionRequest = 10,
	MessageOfTheDayRequest = 18,
	CancelGameCreation = 19,
	CharacterUpgradeRequest = 24,
	CharacterListRequest = 25,
	Invalid = 32
}
