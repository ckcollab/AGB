namespace D2Data;

public enum GameObjectInteractType
{
	GeneralObject = 0,
	Well = 1,
	HealthShrine = 2,
	TrappedChest = 5,
	MonsterChest = 8,
	ManaShrine = 13,
	StaminaShrine = 14,
	ExperienceShrine = 15,
	FireShrine = 19,
	RedPortal = 121,
	LockedChest = 128,
	/// <summary>
	/// Dummy value because for town portals, the intereact type is the destination area...
	/// </summary>
	TownPortal = 255
}
