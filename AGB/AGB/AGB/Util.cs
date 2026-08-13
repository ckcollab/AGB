using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace AGB;

public static class Util
{
	private static Random Rand = new Random();

	public static byte[,] ConvertTwoDimensionalArray(byte[] _source, int _width, int _height)
	{
		byte[,] Destination = new byte[_width, _height];
		for (int i = 0; i < _height; i++)
		{
			for (int j = 0; j < _width; j++)
			{
				Destination[j, i] = _source[i * _width + j];
			}
		}
		return Destination;
	}

	public static byte[] ConvertOneDimensionalArray(byte[,] _source)
	{
		int _width = _source.GetUpperBound(0) + 1;
		int _height = _source.GetUpperBound(1) + 1;
		byte[] Destination = new byte[_width * _height];
		for (int i = 0; i < Destination.Length; i++)
		{
			Destination[i] = _source[i % _width, i / _width];
		}
		return Destination;
	}

	public static string FileRead(string fileName)
	{
		FileStream f = new FileStream(fileName, FileMode.OpenOrCreate);
		StreamReader r = new StreamReader(f);
		string read = r.ReadToEnd();
		r.Close();
		f.Close();
		return read;
	}

	public static void FileWrite(string fileName, string text)
	{
		FileStream f = new FileStream(fileName, FileMode.Create);
		StreamWriter s = new StreamWriter(f);
		s.Write(text);
		s.Close();
		f.Close();
	}

	public static void FileAppend(string fileName, string text)
	{
		FileStream f = new FileStream(fileName, FileMode.Append);
		StreamWriter s = new StreamWriter(f);
		s.Write(text);
		s.Close();
		f.Close();
	}

	public static void DumpCollision(ushort[,] collisionData, int mapX, int mapY, List<Point> path, int width, int height, string fileName)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		Bitmap bitmap = new Bitmap(width, height);
		Image image = (Image)(object)Image.FromHbitmap(bitmap.GetHbitmap());
		Graphics graphics = Graphics.FromImage(image);
		graphics.FillRectangle(Brushes.get_LightBlue(), 0, 0, width, height);
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if ((collisionData[x, y] & 1) > 0)
				{
					graphics.DrawRectangle(new Pen(Brushes.get_Black()), x, y, 1, 1);
				}
			}
		}
		if (path != null)
		{
			Point trailingNode = new Point(-1, -1);
			foreach (Point node in path)
			{
				if (trailingNode.X != -1 && trailingNode.Y != -1)
				{
					graphics.DrawLine(new Pen(Brushes.get_Red()), new Point(node.X - mapX + 2, node.Y - mapY + 2), new Point(trailingNode.X - mapX + 2, trailingNode.Y - mapY + 4));
				}
				graphics.DrawRectangle(new Pen(Brushes.get_Red()), node.X - mapX, node.Y - mapY, 4, 4);
				graphics.FillRectangle(Brushes.get_White(), node.X - mapX + 1, node.Y - mapY + 1, 3, 3);
				trailingNode = node;
			}
		}
		graphics.Save();
		image.Save(fileName + ".png");
	}

	public static void DumpCollision(ushort[,] collisionData, int width, int height, string fileName)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		Bitmap bitmap = new Bitmap(width, height);
		Image image = (Image)(object)Image.FromHbitmap(bitmap.GetHbitmap());
		Graphics graphics = Graphics.FromImage(image);
		graphics.FillRectangle(Brushes.get_LightBlue(), 0, 0, width, height);
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (collisionData[x, y] > 1)
				{
					int alertme = 1;
				}
				if ((collisionData[x, y] & 1) > 0)
				{
					graphics.DrawRectangle(new Pen(Brushes.get_Black()), x, y, 1, 1);
				}
			}
		}
		graphics.Save();
		image.Save(fileName + ".png");
	}

	public static void DumpCollision(ushort[,] collisions, string fileName)
	{
		DumpCollision(collisions, collisions.GetUpperBound(0) + 1, collisions.GetUpperBound(1) + 1, fileName);
	}

	public static void DumpCollision(byte[,] collisionData, string fileName)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		int width = collisionData.GetUpperBound(0) + 1;
		int height = collisionData.GetUpperBound(1) + 1;
		Bitmap bitmap = new Bitmap(width, height);
		Image image = (Image)(object)Image.FromHbitmap(bitmap.GetHbitmap());
		Graphics graphics = Graphics.FromImage(image);
		graphics.FillRectangle(Brushes.get_Black(), 0, 0, width, height);
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (collisionData[x, y] == 1)
				{
					graphics.DrawRectangle(new Pen(Brushes.get_White()), x, y, 1, 1);
				}
			}
		}
		graphics.Save();
		image.Save(fileName + ".png");
	}

	public static Image GetCollisionImage(byte[,] collisions)
	{
		int width = collisions.GetUpperBound(0) + 1;
		int height = collisions.GetUpperBound(1) + 1;
		FastBitmap bitmap = new FastBitmap(width, height);
		bitmap.LockPixels();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (collisions[x, y] == 1)
				{
					bitmap.SetPixel(x, y, Color.White);
				}
				if (collisions[x, y] > 1)
				{
					bitmap.SetPixel(x, y, Color.Green);
					bitmap.SetPixel(x + 1, y, Color.Green);
					bitmap.SetPixel(x - 1, y, Color.Green);
					bitmap.SetPixel(x, y + 1, Color.Green);
				}
			}
		}
		bitmap.UnlockPixels();
		return bitmap;
	}

	public static byte[] Serialize(object o)
	{
		MemoryStream ms = new MemoryStream();
		BinaryFormatter bf1 = new BinaryFormatter();
		bf1.Serialize(ms, o);
		return ms.ToArray();
	}

	public static T Deserialize<T>(byte[] data, int offset, int length)
	{
		MemoryStream ms = new MemoryStream(data, offset, length);
		BinaryFormatter bf1 = new BinaryFormatter();
		ms.Position = 0L;
		return (T)bf1.Deserialize(ms);
	}

	public static void XmlSerialize<T>(object o, string fileName)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		TextWriter writer = new StreamWriter(fileName);
		serializer.Serialize(writer, o);
		writer.Close();
	}

	public static T XmlDeserialize<T>(string fileName)
	{
		TextReader reader = new StreamReader(fileName);
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		object o = serializer.Deserialize(reader);
		reader.Close();
		return (T)o;
	}

	public static string RandomString(int min, int max)
	{
		return RandomString(min, max, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
	}

	public static string RandomString(int min, int max, string allowedChars)
	{
		string s = "";
		int length = Rand.Next(min, max);
		for (int i = 0; i < length; i++)
		{
			s += allowedChars[Rand.Next(allowedChars.Length)];
		}
		return s;
	}

	public static List<string> GetAssemblyFileNames<t>(string directory)
	{
		List<string> assemblyFileNames = new List<string>();
		string[] pluginFiles = Directory.GetFiles(directory, "*.DLL");
		string[] array = pluginFiles;
		foreach (string filePath in array)
		{
			string fileName = Path.GetFileNameWithoutExtension(filePath);
			Type ObjType = null;
			try
			{
				Assembly assembly = Assembly.Load(fileName);
				if ((object)assembly != null)
				{
					Type[] types = assembly.GetTypes();
					Type[] array2 = types;
					foreach (Type type in array2)
					{
						if ((object)type.BaseType == typeof(t))
						{
							ObjType = type;
							break;
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			if ((object)ObjType != null)
			{
				assemblyFileNames.Add(fileName);
			}
		}
		return assemblyFileNames;
	}

	public static T LoadAssembly<T>(string fileName)
	{
		Assembly assembly = Assembly.Load(fileName);
		Type[] types = assembly.GetTypes();
		Type[] array = types;
		foreach (Type type in array)
		{
			if ((object)type.BaseType == typeof(T))
			{
				return (T)assembly.CreateInstance(type.ToString());
			}
		}
		return default(T);
	}
}
