using D2Data;

namespace AGB.D2;

public class Player : Unit
{
	public string Name;

	public CharacterClass Class;

	public Player(Game game)
		: base(game)
	{
	}
}
