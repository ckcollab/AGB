using System;
using System.Collections.Generic;
using AGB.D2;

namespace AGB.Mapping;

public class Room : IDisposable
{
	private unsafe Room2* Room2;

	public ushort X;

	public ushort Y;

	public ushort RelativeX;

	public ushort RelativeY;

	public ushort Width;

	public ushort Height;

	public ushort PresetType;

	public ushort Ds1Def;

	public ushort FileNumber;

	public Ds1 Ds1;

	public List<AGB.D2.PresetUnit> PresetUnits;

	public bool IsPartOfADs1;

	public unsafe Room(Level* level, Room2* room2)
	{
		Room2 = room2;
		X = (ushort)(room2->X * 5);
		Y = (ushort)(room2->Y * 5);
		RelativeX = (ushort)(X - level->X * 5);
		RelativeY = (ushort)(Y - level->Y * 5);
		PresetType = (ushort)room2->PresetType;
		if (PresetType == 2)
		{
			Ds1Def = (ushort)room2->Type2Info->PresetDs1Info->Ds1Def;
			FileNumber = (ushort)room2->Type2Info->PresetDs1Info->FileNumber;
			Ds1 = Tiles.Ds1Sizes[Ds1Def, FileNumber];
		}
		if (PresetType != 2 || MapCache.Instance.Rooms[Ds1Def, FileNumber] == null)
		{
			Width = (ushort)room2->Room1->Coll->dwSizeGameX;
			Height = (ushort)room2->Room1->Coll->dwSizeGameY;
			PresetUnits = new List<AGB.D2.PresetUnit>();
			for (PresetUnit* presetUnit = room2->PresetUnit; presetUnit != null; presetUnit = presetUnit->Next)
			{
				PresetUnits.Add(new AGB.D2.PresetUnit
				{
					Id = (ushort)presetUnit->nTxtFileNo,
					Type = presetUnit->Type,
					X = (ushort)presetUnit->X,
					Y = (ushort)presetUnit->Y
				});
			}
		}
	}

	public void Dispose()
	{
	}
}
