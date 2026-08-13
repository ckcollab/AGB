namespace D2Data;

public class CharactersInfo
{
	/// <summary>
	/// Character Genders array. true = Female; false = Male
	/// </summary>
	public static readonly bool[] Gender = new bool[7] { true, true, false, false, false, false, true };

	public static readonly double[] LifePerVitality = new double[7] { 3.0, 2.0, 2.0, 3.0, 4.0, 2.0, 3.0 };

	public static readonly double[] StaminaPerVitality = new double[7] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.25 };

	public static readonly double[] ManaPerEnergy = new double[7] { 1.5, 2.0, 2.0, 1.5, 1.0, 2.0, 1.75 };
}
