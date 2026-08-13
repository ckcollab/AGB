namespace AGB.D2.Net.Packets;

public enum PacketType : byte
{
	Welcome,
	WelcomeResult,
	Login,
	LoginResult,
	SetNewGameInfo,
	SetNewGameInfoResult,
	GetPath,
	GetPathResult,
	GetMap,
	GetMapResult,
	Message,
	Ping,
	Pong,
	Quit
}
