using System;
using D2Packets.BnetServer;
using ETUtils;

namespace D2Packets.RealmClient;

/// <summary>
/// Realm Client Packet 0x01 - Realm Startup Request
/// <para>Request realm connection startup using the information from 
/// <see cref="T:D2Packets.BnetServer.RealmLogonResponse" />.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.RealmServer.RealmStartupResponse" />
/// </remarks>
public class RealmStartupRequest : RCPacket
{
	protected uint cookie;

	protected string username;

	/// <summary>
	/// Battle.net unique username. This is the account name plus #X if it conflicts with someone else's.
	/// <list type="bullet">
	/// <item>Offset: 67</item>
	/// <item>Length: (null terminated string)</item>
	/// </list>
	/// </summary>
	public string Username => username;

	/// <summary>
	/// The <see cref="P:D2Packets.BnetServer.RealmLogonResponse.Cookie" /> from <see cref="T:D2Packets.BnetServer.RealmLogonResponse" />.
	/// <para>This is the first DWORD of <see cref="P:D2Packets.RealmClient.RealmStartupRequest.StartupData" />.</para>
	/// <list type="bullet">
	/// <item>Offset: 3</item>
	/// <item>Length: DWORD</item>
	/// </list>
	/// </summary>
	public uint Cookie => cookie;

	/// <summary>
	/// The data provided by Bnet server in <see cref="T:D2Packets.BnetServer.RealmLogonResponse" />.
	/// <para>This contains the <see cref="P:D2Packets.RealmClient.RealmStartupRequest.Cookie" /> at the start.</para>
	/// <list type="bullet">
	/// <item>Offset: 3</item>
	/// <item>Length: 64 bytes</item>
	/// </list>
	/// </summary>
	public byte[] StartupData
	{
		get
		{
			byte[] bytes = new byte[64];
			Array.Copy(data, 3, bytes, 0, 64);
			return bytes;
		}
	}

	public RealmStartupRequest(byte[] data)
		: base(data)
	{
		cookie = BitConverter.ToUInt32(data, 3);
		username = ByteConverter.GetNullString(data, 67);
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.RealmClient.RealmStartupRequest.StartupData" /> contained in this packet with the one in specified packet.
	/// </summary>
	/// <param name="realmLogon">The packet to compare with.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(RealmLogonResponse realmLogon)
	{
		return realmLogon.CompareStartupData(data, 3);
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.RealmClient.RealmStartupRequest.StartupData" /> contained in this packet with the provided data.
	/// </summary>
	/// <param name="bytes">Byte array containing the data to compare with.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(byte[] bytes)
	{
		for (int i = 0; i < 64; i++)
		{
			if (data[i + 3] != bytes[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Compares the <see cref="P:D2Packets.RealmClient.RealmStartupRequest.StartupData" /> contained in this packet with the provided data.
	/// </summary>
	/// <param name="bytes">Byte array containing the data to compare with.</param>
	/// <param name="offset">Offset in <paramref name="bytes" /> to start reading data at.</param>
	/// <returns>true if the data is the same, false otherwise.</returns>
	public bool CompareStartupData(byte[] bytes, int offset)
	{
		for (int i = 0; i < 64; i++)
		{
			if (data[i + 3] != bytes[i + offset])
			{
				return false;
			}
		}
		return true;
	}
}
