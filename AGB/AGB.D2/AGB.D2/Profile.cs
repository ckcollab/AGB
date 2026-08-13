using D2Data;

namespace AGB.D2;

public class Profile
{
	public string Username;

	public string Password;

	public BattleNetClient Client = BattleNetClient.Diablo2LoD;

	public GameDifficulty Difficulty;

	public Character Character;

	public CdKeySetProxyCombo CdKeySetProxyCombo;
}
