using System;
using System.IO;
using D2Data;
using MBNCSUtil;

namespace AGB.D2;

public class MapCache
{
	private int InitialCacheSize;

	public static readonly MapCache Instance;

	public CachedRoom[,] Rooms;

	static MapCache()
	{
		Instance = new MapCache();
	}

	private MapCache()
	{
		Rooms = new CachedRoom[1090, 6];
		FileStream fStream = File.Open("mapcache.cache", FileMode.OpenOrCreate, FileAccess.Read);
		fStream.Seek(0L, SeekOrigin.Begin);
		if (fStream.Length == 0)
		{
			return;
		}
		byte[] data = new byte[fStream.Length];
		fStream.Read(data, 0, (int)fStream.Length);
		int index = 0;
		int roomCount = BitConverter.ToInt32(data, index);
		index += 4;
		for (int i = 0; i < roomCount; i++)
		{
			CachedRoom room = new CachedRoom
			{
				Id1 = BitConverter.ToUInt16(data, index)
			};
			index += 2;
			room.Id2 = BitConverter.ToUInt16(data, index);
			index += 2;
			room.Width = BitConverter.ToUInt16(data, index);
			index += 2;
			room.Height = BitConverter.ToUInt16(data, index);
			index += 2;
			PresetUnit[] presetUnits = new PresetUnit[BitConverter.ToUInt16(data, index)];
			index += 2;
			int colLength = BitConverter.ToInt32(data, index);
			index += 4;
			for (int p = 0; p < presetUnits.Length; p++)
			{
				presetUnits[p] = new PresetUnit();
				presetUnits[p].Id = BitConverter.ToUInt16(data, index);
				index += 2;
				presetUnits[p].Type = (UnitType)data[index];
				index++;
				presetUnits[p].X = BitConverter.ToUInt16(data, index);
				index += 2;
				presetUnits[p].Y = BitConverter.ToUInt16(data, index);
				index += 2;
			}
			room.PresetUnits = presetUnits;
			fStream.Seek(index, SeekOrigin.Begin);
			byte[] colData = new byte[colLength];
			fStream.Read(colData, 0, colLength);
			index += colLength;
			room.Collisions = Util.ConvertTwoDimensionalArray(colData, room.Width, room.Height);
			Rooms[room.Id1, room.Id2] = room;
		}
		fStream.Close();
		InitialCacheSize = GetElementCount();
	}

	public void Init()
	{
	}

	public void Save()
	{
		FileStream fStream = File.Open("mapcache.cache", FileMode.OpenOrCreate);
		fStream.Seek(0L, SeekOrigin.Begin);
		DataBuffer header = new DataBuffer();
		header.InsertInt32(GetElementCount());
		header.WriteToOutputStream(fStream);
		if (Rooms != null)
		{
			for (int id1 = 0; id1 < 1090; id1++)
			{
				for (int id2 = 0; id2 < 6; id2++)
				{
					DataBuffer dataBuffer = new DataBuffer();
					if (Rooms[id1, id2] != null)
					{
						dataBuffer.InsertInt16((short)id1);
						dataBuffer.InsertInt16((short)id2);
						dataBuffer.InsertUInt16(Rooms[id1, id2].Width);
						dataBuffer.InsertUInt16(Rooms[id1, id2].Height);
						dataBuffer.InsertUInt16((ushort)Rooms[id1, id2].PresetUnits.Length);
						byte[] col = Util.ConvertOneDimensionalArray(Rooms[id1, id2].Collisions);
						dataBuffer.InsertInt32(col.Length);
						PresetUnit[] presetUnits = Rooms[id1, id2].PresetUnits;
						foreach (PresetUnit p in presetUnits)
						{
							dataBuffer.InsertUInt16(p.Id);
							dataBuffer.InsertByte((byte)p.Type);
							dataBuffer.InsertUInt16(p.X);
							dataBuffer.InsertUInt16(p.Y);
						}
						dataBuffer.InsertByteArray(col);
						dataBuffer.WriteToOutputStream(fStream);
					}
				}
			}
		}
		fStream.Close();
	}

	public int GetElementCount()
	{
		int count = 0;
		for (int x = 0; x < 1090; x++)
		{
			for (int y = 0; y < 6; y++)
			{
				if (Rooms[x, y] != null)
				{
					count++;
				}
			}
		}
		return count;
	}
}
