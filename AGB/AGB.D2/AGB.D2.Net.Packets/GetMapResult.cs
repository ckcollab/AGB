using System;
using System.Collections.Generic;
using D2Data;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public class GetMapResult : AGBPacket
{
	public GetMapResultValue Result;

	public Map Map;

	public override PacketType Type => PacketType.GetMapResult;

	public override byte[] Data
	{
		get
		{
			DataBuffer dataBuffer = new DataBuffer();
			if (Map == null)
			{
				return new byte[0];
			}
			dataBuffer.InsertUInt16((ushort)Map.AreaLevel);
			dataBuffer.InsertUInt16(Map.X);
			dataBuffer.InsertUInt16(Map.Y);
			dataBuffer.InsertUInt16(Map.Width);
			dataBuffer.InsertUInt16(Map.Height);
			dataBuffer.InsertUInt16((ushort)Map.Rooms.Count);
			foreach (Room room in Map.Rooms)
			{
				dataBuffer.InsertUInt16(room.X);
				dataBuffer.InsertUInt16(room.Y);
				dataBuffer.InsertUInt16(room.Id1);
				dataBuffer.InsertUInt16(room.Id2);
				if (room.Id1 == 0 || MapCache.Instance.Rooms[room.Id1, room.Id2] == null)
				{
					int width = room.Collisions.GetUpperBound(0) + 1;
					int height = room.Collisions.GetUpperBound(1) + 1;
					dataBuffer.InsertUInt16((ushort)width);
					dataBuffer.InsertUInt16((ushort)height);
					byte[] data = Util.ConvertOneDimensionalArray(room.Collisions);
					data = AGBPacket.DeflatePacket(data);
					if (data.Length >= 32768)
					{
						dataBuffer.InsertUInt16((ushort)(((uint)data.Length & 0x7FFFu) | 0x8000u));
						dataBuffer.InsertUInt16((ushort)(data.Length >> 15));
					}
					else
					{
						dataBuffer.InsertUInt16((ushort)data.Length);
					}
					dataBuffer.InsertByteArray(data);
					dataBuffer.InsertUInt16((ushort)room.PresetUnits.Length);
					PresetUnit[] presetUnits = room.PresetUnits;
					foreach (PresetUnit p in presetUnits)
					{
						dataBuffer.InsertUInt16(p.Id);
						dataBuffer.InsertByte((byte)p.Type);
						dataBuffer.InsertUInt16(p.X);
						dataBuffer.InsertUInt16(p.Y);
					}
				}
			}
			return dataBuffer.GetData();
		}
	}

	public static GetMapResult Parse(byte[] packetData, int offset)
	{
		if (packetData.Length == 5)
		{
			return null;
		}
		byte[] data = new byte[packetData.Length - offset];
		Array.Copy(packetData, offset, data, 0, packetData.Length - offset);
		DataReader reader = new DataReader(data);
		reader.Seek(0);
		List<Map> maps = new List<Map>();
		AreaLevel Area = (AreaLevel)reader.ReadInt16();
		ushort x = reader.ReadUInt16();
		ushort y = reader.ReadUInt16();
		ushort width = reader.ReadUInt16();
		ushort height = reader.ReadUInt16();
		int rooms = reader.ReadUInt16();
		List<Room> Rooms = new List<Room>();
		for (int i = 0; i < rooms; i++)
		{
			Room room = new Room();
			room.X = reader.ReadUInt16();
			room.Y = reader.ReadUInt16();
			room.Id1 = reader.ReadUInt16();
			room.Id2 = reader.ReadUInt16();
			if (room.Id1 == 0 || MapCache.Instance.Rooms[room.Id1, room.Id2] == null)
			{
				int roomWidth = reader.ReadInt16();
				int roomHeight = reader.ReadInt16();
				int data_size = reader.ReadInt16();
				if ((data_size & 0x8000) == 2048)
				{
					data_size = (data_size & 0x7FFF) + (reader.ReadInt16() << 15);
				}
				byte[] collisions = reader.ReadByteArray(data_size);
				collisions = AGBPacket.InflatePacket(collisions);
				room.Collisions = Util.ConvertTwoDimensionalArray(collisions, roomWidth, roomHeight);
				room.PresetUnits = new PresetUnit[reader.ReadUInt16()];
				for (int p = 0; p < room.PresetUnits.Length; p++)
				{
					room.PresetUnits[p] = new PresetUnit();
					room.PresetUnits[p].Id = reader.ReadUInt16();
					room.PresetUnits[p].Type = (UnitType)reader.ReadByte();
					room.PresetUnits[p].X = reader.ReadUInt16();
					room.PresetUnits[p].Y = reader.ReadUInt16();
				}
			}
			Rooms.Add(room);
		}
		GetMapResult getMapResult = new GetMapResult();
		getMapResult.Result = GetMapResultValue.Success;
		getMapResult.Map = new Map(Area, Rooms, x, y, width, height);
		return getMapResult;
	}
}
