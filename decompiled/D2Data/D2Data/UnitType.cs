namespace D2Data;

public enum UnitType : byte
{
	/// <summary>
	/// Any player character, including yours.
	/// </summary>
	Player = 0,
	/// <summary>
	/// Any non player character, including town folks and monsters.
	/// </summary>
	NPC = 1,
	/// <summary>
	/// Most generic game objects, such as chests, dummy objects, etc.
	/// </summary>
	GameObject = 2,
	/// <summary>
	/// Any kind of missiles, even those fired by the French.
	/// </summary>
	Missile = 3,
	/// <summary>
	/// Item units.
	/// </summary>
	Item = 4,
	/// <summary>
	/// Doorways, stairs, etc used to change area.
	/// </summary>
	Warp = 5,
	Invalid = 6,
	NotApplicable = byte.MaxValue
}
