using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class ConnectionRequest
{
	public ProtocolId _ProtocolId;

	public string _PlatformId;

	public string _ProductId;

	public Version _Version;

	public ProductLanguage _ProductLanguage;

	public TimeZone _TimeZone;

	public LocalId _LocalId;

	public LanguageId _LanguageId;

	public Country _Country;

	private uint LocalIP = 167946432u;

	private string CountryAbvr = "CAN";

	private string CountryString = "Canada";

	public byte[] Data;

	public ConnectionRequest()
	{
		_ProtocolId = ProtocolId.Default;
		_PlatformId = "IX86";
		_ProductId = "D2XP";
		_Version = Version.Default;
		_ProductLanguage = ProductLanguage.English;
		_TimeZone = TimeZone.Est;
		_LocalId = LocalId.Default;
		_LanguageId = LanguageId.Default;
		_Country = Country.Canada;
		Data = Build();
	}

	public ConnectionRequest(uint LocalIP, string CountryAbvr, string CountryString)
	{
		_ProtocolId = ProtocolId.Default;
		_PlatformId = "IX86";
		_ProductId = "D2XP";
		_Version = Version.Default;
		_ProductLanguage = ProductLanguage.English;
		_TimeZone = TimeZone.Est;
		_LocalId = LocalId.Default;
		_LanguageId = LanguageId.Default;
		_Country = Country.Canada;
		this.LocalIP = LocalIP;
		this.CountryAbvr = CountryAbvr;
		this.CountryString = CountryString;
		Data = Build();
	}

	public ConnectionRequest(string PlatformId, string ProductId, Country country, ProtocolId protocolid, Version version, ProductLanguage productlanguage, TimeZone timezone, LocalId localid, LanguageId languageid)
	{
		_ProtocolId = protocolid;
		_PlatformId = PlatformId;
		_ProductId = ProductId;
		_Version = version;
		_ProductLanguage = productlanguage;
		_TimeZone = timezone;
		_LocalId = localid;
		_LanguageId = languageid;
		_Country = country;
		Data = Build();
	}

	public ConnectionRequest(string PlatformId, string ProductId, Country country, ProtocolId protocolid, Version version, ProductLanguage productlanguage, TimeZone timezone, LocalId localid, LanguageId languageid, uint LocalIP, string CountryAbvr, string CountryString)
	{
		_ProtocolId = protocolid;
		_PlatformId = PlatformId;
		_ProductId = ProductId;
		_Version = version;
		_ProductLanguage = productlanguage;
		_TimeZone = timezone;
		_LocalId = localid;
		_LanguageId = languageid;
		_Country = country;
		this.LocalIP = LocalIP;
		this.CountryAbvr = CountryAbvr;
		this.CountryString = CountryString;
		Data = Build();
	}

	public byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(80);
		myPacket.InsertInt32((int)_ProtocolId);
		myPacket.InsertDwordString(_PlatformId);
		myPacket.InsertDwordString(_ProductId);
		myPacket.InsertInt32((int)_Version);
		myPacket.InsertInt32((int)_ProductLanguage);
		myPacket.InsertInt32((int)LocalIP);
		myPacket.InsertInt32((int)_TimeZone);
		myPacket.InsertInt32((int)_LocalId);
		myPacket.InsertInt32((int)_LanguageId);
		myPacket.InsertCString(CountryAbvr);
		myPacket.InsertCString(CountryString);
		return myPacket.GetData();
	}
}
