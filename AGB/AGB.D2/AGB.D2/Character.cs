using D2Data;

namespace AGB.D2;

public class Character
{
	public Realm Realm;

	public string Name;

	public CharacterClass Class;

	public Character()
	{
	}

	public Character(string name, Realm realm, CharacterClass charClass)
	{
		Name = name;
		Realm = realm;
		Class = charClass;
	}
}
