using System;
using System.Net;
using D2Data;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x50 - Bnet Connection Request
/// <para>First packet sent to bnet to establish a connection.</para>
/// </summary>
/// <remarks>
/// Response: <see cref="T:D2Packets.BnetServer.BnetConnectionResponse" />
/// </remarks>
public class BnetConnectionRequest : BCPacket
{
	protected static uint CurrentD2Version = 11u;

	protected static uint CurrentD2LoDVersion = 11u;

	protected uint protocol;

	protected BattleNetPlatform platform;

	protected BattleNetClient client;

	protected uint version;

	protected uint language;

	protected IPAddress localIP;

	protected uint timeZoneBias;

	protected uint localeID;

	protected uint languageID;

	protected string countryAbbreviation;

	protected string countryName;

	public uint Protocol => protocol;

	public BattleNetPlatform Platform => platform;

	public BattleNetClient Client => client;

	public uint Version => version;

	public uint Language => language;

	public IPAddress LocalIP => localIP;

	public uint TimeZoneBias => timeZoneBias;

	public uint LocaleID => localeID;

	public uint LanguageID => languageID;

	public string CountryAbbreviation => countryAbbreviation;

	public string CountryName => countryName;

	public BnetConnectionRequest(byte[] data)
		: base(data)
	{
		protocol = BitConverter.ToUInt32(data, 4);
		platform = (BattleNetPlatform)BitConverter.ToUInt32(data, 8);
		client = (BattleNetClient)BitConverter.ToUInt32(data, 12);
		version = BitConverter.ToUInt32(data, 16);
		language = BitConverter.ToUInt32(data, 20);
		localIP = new IPAddress(BitConverter.ToUInt32(data, 24));
		timeZoneBias = BitConverter.ToUInt32(data, 28);
		localeID = BitConverter.ToUInt32(data, 32);
		languageID = BitConverter.ToUInt32(data, 36);
		countryAbbreviation = ByteConverter.GetNullString(data, 40);
		countryName = ByteConverter.GetNullString(data, 41 + countryAbbreviation.Length);
	}
}
