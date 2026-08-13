using System;
using System.IO;
using System.IO.Compression;
using MBNCSUtil;

namespace AGB.D2.Net.Packets;

public abstract class AGBPacket
{
	public abstract PacketType Type { get; }

	public virtual byte[] Data => Util.Serialize(this);

	public static AGBPacket Parse(byte[] data)
	{
		return data[0] switch
		{
			0 => new Welcome(), 
			1 => WelcomeResult.Parse(data, 5), 
			2 => Login.Parse(data, 5), 
			3 => LoginResult.Parse(data, 5), 
			4 => SetNewGameInfo.Parse(data, 5), 
			5 => SetNewGameInfoResult.Parse(data, 5), 
			8 => GetMap.Parse(data, 5), 
			9 => GetMapResult.Parse(data, 5), 
			10 => Message.Parse(data, 5), 
			11 => new Ping(), 
			12 => new Pong(), 
			13 => new Quit(), 
			_ => null, 
		};
	}

	public static byte[] DeflatePacket(byte[] Buffer)
	{
		if (Buffer.Length <= 1)
		{
			return Buffer;
		}
		MemoryStream ms = new MemoryStream();
		GZipStream gs = new GZipStream(ms, CompressionMode.Compress, leaveOpen: true);
		gs.Write(Buffer, 0, Buffer.Length);
		gs.Close();
		return ms.ToArray();
	}

	public static byte[] InflatePacket(byte[] Buffer)
	{
		if (Buffer.Length <= 1)
		{
			return Buffer;
		}
		MemoryStream input = new MemoryStream();
		input.Write(Buffer, 0, Buffer.Length);
		input.Position = 0L;
		GZipStream gzip = new GZipStream(input, CompressionMode.Decompress, leaveOpen: true);
		MemoryStream output = new MemoryStream();
		byte[] buff = new byte[64];
		int read = -1;
		for (read = gzip.Read(buff, 0, buff.Length); read > 0; read = gzip.Read(buff, 0, buff.Length))
		{
			output.Write(buff, 0, read);
		}
		gzip.Close();
		return output.ToArray();
	}

	public static byte[] Construct(AGBPacket p)
	{
		DataBuffer buffer = new DataBuffer();
		byte[] data = p.Data;
		buffer.InsertByte((byte)p.Type);
		buffer.InsertInt32(data.Length + 5);
		buffer.InsertByteArray(data);
		return buffer.GetData();
	}

	public override string ToString()
	{
		return Enum.GetName(typeof(PacketType), Type)!.ToUpperInvariant();
	}
}
