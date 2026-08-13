using System;
using System.Net;
using D2Packets.RealmClient;
using ETUtils;

namespace D2Packets.BnetServer;

/// <summary>
/// Bnet Server Packet 0x3E - Realm Logon Response
/// <para>Supplies the data necessary to connect to a Realm server.</para>
/// <para>If the request is not successful, only <see cref="P:D2Packets.BnetServer.RealmLogonResponse.Cookie" /> and <see cref="P:D2Packets.BnetServer.RealmLogonResponse.Result" /> will be set.</para>
/// </summary>
/// <remarks>
/// Response to: <see cref="T:D2Packets.BnetClient.RealmLogonRequest" />
/// </remarks>
public class RealmLogonResponse : BSPacket
{
	public static readonly int NULL_Int32 = -1;

	protected uint cookie;

	protected RealmLogonResult result;

	protected IPAddress realmServerIP = null;

	protected int realmServerPort = -1;

	protected string username = null;

	protected ushort unknown;

	public uint Status;

	public uint[] MCPChunk1 = new uint[2];

	public uint[] MCPChunk2 = new uint[12];

	/// <summary>
	/// If the logon is not successful, this is the error code from packet.
	/// <para>Otherwise, it is a dummy value: RealmLogonResult.Success.</para>
	/// <list type="bullet">
	/// <item>Offset: 8</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public RealmLogonResult Result => result;

	/// <summary>
	/// The IP of the Realm server to connect to, or null if the request fails.
	/// <list type="bullet">
	/// <item>Offset: 20</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public IPAddress RealmServerIP
	{
		get
		{
			return realmServerIP;
		}
		set
		{
			IsWritableEx();
			realmServerIP = value;
			byte[] addy = value.GetAddressBytes();
			data[20] = addy[0];
			data[21] = addy[1];
			data[22] = addy[2];
			data[23] = addy[3];
		}
	}

	/// <summary>
	/// The IP of the Realm server to connect to, or -1 if the request fails.
	/// <list type="bullet">
	/// <item>Offset: 24</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public int RealmServerPort
	{
		get
		{
			return realmServerPort;
		}
		set
		{
			IsWritableEx();
			realmServerPort = value;
			data[24] = (byte)(value >> 8);
			data[25] = (byte)value;
		}
	}

	/// <summary>
	/// Battle.net unique username. This is the account name plus #X if it conflicts with someone else's.
	/// <list type="bullet">
	/// <item>Offset: 76</item>
	/// <item>Length: (null terminated string)</item>
	/// </list>
	/// </summary>
	public string Username => username;

	/// <summary>
	/// The <see cref="P:D2Packets.BnetClient.RealmLogonRequest.Cookie" /> of the requesting <see cref="T:D2Packets.BnetClient.RealmLogonRequest" />.
	/// <para>This is the first DWORD of <see cref="P:D2Packets.BnetServer.RealmLogonResponse.StartupData" />.</para>
	/// <list type="bullet">
	/// <item>Offset: 4</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public uint Cookie => cookie;

	/// <summary>
	/// Use is unknown.
	/// <list type="bullet">
	/// <item>Offset: 77 + <see cref="P:D2Packets.BnetServer.RealmLogonResponse.Username" />.Length</item>
	/// <item>Length: (null terminated string)</item>
	/// </list>
	/// </summary>
	public ushort Unknown => unknown;

	/// <summary>
	/// The data to send to Realm in <see cref="T:D2Packets.RealmClient.RealmStartupRequest" />.
	/// <para>This contains the <see cref="P:D2Packets.BnetServer.RealmLogonResponse.Cookie" /> at the start.</para>
	/// <para>Postion: bytes 4-19 and 28-75</para>
	/// </summary>
	public byte[] StartupData
	{
		get
		{
			if (result != 0)
			{
				return null;
			}
			byte[] bytes = new byte[64];
			Array.Copy(data, 4, bytes, 0, 16);
			Array.Copy(data, 28, bytes, 16, 48);
			return bytes;
		}
	}

	public RealmLogonResponse(byte[] data)
		: base(data)
	{
		cookie = BitConverter.ToUInt32(data, 4);
		if (base.data.Length < 75)
		{
			result = (RealmLogonResult)BitConverter.ToUInt32(data, 8);
			return;
		}
		result = RealmLogonResult.Success;
		Status = BitConverter.ToUInt32(data, 8);
		MCPChunk1[0] = BitConverter.ToUInt32(data, 12);
		MCPChunk1[1] = BitConverter.ToUInt32(data, 16);
		realmServerIP = new IPAddress(BitConverter.ToUInt32(data, 20));
		realmServerPort = BEBitConverter.ToUInt16(data, 24);
		for (int i = 0; i < 12; i++)
		{
			MCPChunk2[i] = BitConverter.ToUInt32(data, 28 + i * 4);
		}
		username = ByteConverter.GetNullString(data, 76);
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.BnetServer.RealmLogonResponse.StartupData" /> contained in this packet with the one in specified packet.
	/// </summary>
	/// <param name="realmStartup">The packet to compare with.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(RealmStartupRequest realmStartup)
	{
		return realmStartup.CompareStartupData(this);
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.BnetServer.RealmLogonResponse.StartupData" /> contained in this packet with the provided data.
	/// </summary>
	/// <param name="bytes">Byte array containing the data to compare with.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(byte[] bytes)
	{
		return CompareStartupData(bytes, 0);
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.BnetServer.RealmLogonResponse.StartupData" /> contained in this packet with the provided data.
	/// </summary>
	/// <param name="bytes">Byte array containing the data to compare with.</param>
	/// <param name="offset">Offset in <paramref name="bytes" /> to start reading data at.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(byte[] bytes, int offset)
	{
		for (int i = 0; i < 16; i++)
		{
			if (data[i + 3] != bytes[i + offset])
			{
				return false;
			}
		}
		offset += 16;
		for (int i = 0; i < 48; i++)
		{
			if (data[i + 27] != bytes[i + offset])
			{
				return false;
			}
		}
		return true;
	}
}
