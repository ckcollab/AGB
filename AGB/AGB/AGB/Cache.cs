using System;
using System.IO;
using System.IO.Compression;

namespace AGB;

public class Cache
{
	public static T Load<T>(string fileName)
	{
		if (!File.Exists(fileName))
		{
			throw new Exception("Cache missing: '" + fileName + "'");
		}
		FileStream fstream = File.Open(fileName, FileMode.Open);
		byte[] data = new byte[fstream.Length];
		fstream.Read(data, 0, (int)fstream.Length);
		fstream.Close();
		if (data.Length == 0)
		{
			return default(T);
		}
		MemoryStream input = new MemoryStream();
		input.Write(data, 0, data.Length);
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
		byte[] uncompressed = output.ToArray();
		return Util.Deserialize<T>(uncompressed, 0, uncompressed.Length);
	}

	public static void Save(object o, string fileName)
	{
		MemoryStream ms = new MemoryStream();
		GZipStream gs = new GZipStream(ms, CompressionMode.Compress, leaveOpen: true);
		byte[] serializeData = Util.Serialize(o);
		gs.Write(serializeData, 0, serializeData.Length);
		gs.Close();
		byte[] zippedData = ms.ToArray();
		ms.Close();
		FileStream fStream = File.Open(fileName, FileMode.OpenOrCreate);
		fStream.Write(zippedData, 0, zippedData.Length);
		fStream.Close();
	}
}
