namespace D2Data;

public enum UnitVisibility
{
	/// <summary>
	/// No longer valid (not in proximity, the area for which the client receives extended information.)
	/// </summary>
	Invalid = 0,
	/// <summary>
	/// Displayed on screen.
	/// </summary>
	OnScreen = 1,
	/// <summary>
	/// Visible to the character.
	/// </summary>
	InSight = 2,
	/// <summary>
	/// Within the 2-4 screen range for which the client receives extended information.
	/// </summary>
	InProximity = 4
}
