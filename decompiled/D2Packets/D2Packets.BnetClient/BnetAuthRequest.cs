using System;
using D2Packets.D2Packets;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x51 - Bnet Auth Request
/// <para>Second packet sent to bnet containing CDKey and other auth info.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.BnetAuthResponse" />
/// </remarks>
public class BnetAuthRequest : BCPacket
{
	protected int clientToken;

	protected int gameVersion;

	protected int gameHash;

	protected int keyCount;

	protected int useSpawn;

	protected CDKeyInfo classicKey;

	protected CDKeyInfo expansionKey;

	protected string gameInfo;

	protected string ownerName;

	public string GameInfo => gameInfo;

	/// <summary>
	/// The name associated with the CD Keys.
	/// </summary>
	public string OwnerName
	{
		get
		{
			return ownerName;
		}
		set
		{
			IsWritableEx();
			int offset = 25 + keyCount * 36 + gameInfo.Length;
			if (ownerName.Length == value.Length)
			{
				for (int i = 0; i < value.Length; i++)
				{
					data[offset++] = (byte)value[i];
				}
			}
			else
			{
				int length = offset + value.Length + 1;
				byte[] newData = new byte[length];
				Array.Copy(data, newData, offset);
				data[2] = (byte)length;
				data[3] = (byte)(length >> 8);
				for (int i = 0; i < value.Length; i++)
				{
					newData[offset++] = (byte)value[i];
				}
				data = newData;
			}
			ownerName = value;
		}
	}

	/// <summary>
	/// Number of CD Keys in this packet. Should be 1 for Classic and 2 for Expansion
	/// </summary>
	public int KeyCount => keyCount;

	public CDKeyInfo ClassicKey
	{
		get
		{
			return classicKey;
		}
		set
		{
			IsWritableEx();
			if (keyCount < 1)
			{
				throw new ArgumentOutOfRangeException("value", value, "Cannot write classic key!");
			}
			classicKey = value;
			writeKey(classicKey, data, 24);
		}
	}

	public CDKeyInfo ExpansionKey
	{
		get
		{
			return expansionKey;
		}
		set
		{
			IsWritableEx();
			if (keyCount < 2)
			{
				throw new ArgumentOutOfRangeException("value", value, "Cannot write expansion key in classic login!");
			}
			expansionKey = value;
			writeKey(expansionKey, data, 60);
		}
	}

	public int GameVersion => gameVersion;

	/// <summary>
	/// As returned by CheckRevision.
	/// </summary>
	public int GameHash => gameHash;

	public int ClientToken => clientToken;

	/// <summary>
	/// Always 0 for Diablo... only true (1) for Starcraft and Warcraft II.
	/// </summary>
	public int UseSpawn => useSpawn;

	public BnetAuthRequest(byte[] data)
		: base(data)
	{
		clientToken = BitConverter.ToInt32(data, 4);
		gameVersion = BitConverter.ToInt32(data, 8);
		gameHash = BitConverter.ToInt32(data, 12);
		keyCount = BitConverter.ToInt32(data, 16);
		useSpawn = BitConverter.ToInt32(data, 20);
		int offset = 24;
		if (keyCount > 0)
		{
			classicKey = new CDKeyInfo(data, offset);
			offset += 36;
		}
		if (keyCount > 1)
		{
			expansionKey = new CDKeyInfo(data, offset);
			offset += 36;
		}
		gameInfo = ByteConverter.GetNullString(data, offset);
		ownerName = ByteConverter.GetNullString(data, offset + 1 + gameInfo.Length);
	}

	public BnetAuthRequest(int clientToken, int gameVersion, int gameHash, CDKeyInfo classicKey, CDKeyInfo expansionKey, string gameInfo, string ownerName)
		: base(Build(clientToken, gameVersion, gameHash, classicKey, expansionKey, gameInfo, ownerName))
	{
		this.clientToken = clientToken;
		this.gameVersion = gameVersion;
		this.gameHash = gameHash;
		keyCount = ((expansionKey == null) ? 1 : 2);
		this.classicKey = classicKey;
		this.expansionKey = expansionKey;
		useSpawn = 0;
		this.gameInfo = gameInfo;
		this.ownerName = ownerName;
	}

	public unsafe static byte[] Build(int clientToken, int gameVersion, int gameHash, CDKeyInfo classicKey, CDKeyInfo expansionKey, string gameInfo, string ownerName)
	{
		if (classicKey == null)
		{
			throw new ArgumentNullException("classicKey");
		}
		if (gameInfo == null)
		{
			throw new ArgumentNullException("gameInfo");
		}
		if (ownerName == null)
		{
			throw new ArgumentNullException("ownerName");
		}
		int keyCount = ((expansionKey == null) ? 1 : 2);
		int length = 26 + keyCount * 36 + gameInfo.Length + ownerName.Length;
		byte[] data = new byte[length];
		data[0] = byte.MaxValue;
		data[1] = 81;
		data[2] = (byte)length;
		data[3] = (byte)(length >> 8);
		int offset = 4;
		byte* p = (byte*)(&clientToken);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		p = (byte*)(&gameVersion);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		p = (byte*)(&gameHash);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		p = (byte*)(&keyCount);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		offset += 4;
		writeKey(classicKey, data, offset);
		offset += 36;
		if (expansionKey != null)
		{
			writeKey(expansionKey, data, offset);
			offset += 36;
		}
		for (int i = 0; i < gameInfo.Length; i++)
		{
			data[offset++] = (byte)gameInfo[i];
		}
		offset++;
		for (int i = 0; i < ownerName.Length; i++)
		{
			data[offset++] = (byte)ownerName[i];
		}
		return data;
	}

	private unsafe static void writeKey(CDKeyInfo key, byte[] data, int offset)
	{
		int val = key.Length;
		byte* p = (byte*)(&val);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		val = key.ProductValue;
		p = (byte*)(&val);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		val = key.PublicValue;
		p = (byte*)(&val);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		val = key.Unknown;
		p = (byte*)(&val);
		for (int i = 0; i < 4; i++)
		{
			data[offset++] = p[i];
		}
		for (int i = 0; i < 20; i++)
		{
			data[offset++] = key.Hash[i];
		}
	}
}
