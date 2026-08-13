using MBNCSUtil;

namespace AGB.D2.Net.D2.BC;

public class AuthorizationCheck
{
	public uint _ClientToken;

	private int _EXEVersion;

	private int _EXEHash;

	private int _NumberOfKeys = 2;

	private int _UsingSpawnBoolean = 0;

	private int _KeyLength1;

	private int _CDKeyProductValue1;

	private int _CDKeyPublicValue1;

	private int _Unknown1 = 0;

	private byte[] _HashedKeyData1;

	private int _KeyLength2;

	private int _CDKeyProductValue2;

	private int _CDKeyPublicValue2;

	private int _Unknown2 = 0;

	private byte[] _HashedKeyData2;

	private string _EXEInfo = "Game.exe 08/17/05 02:12:38 2129920";

	private string _CDKeyOwnerName;

	public byte[] Data;

	public AuthorizationCheck(uint clienttoken, string filename, string valuestring, string[] gamefiles, CdKey classic, CdKey lod, uint servertoken, string ownername)
	{
		_ClientToken = clienttoken;
		int MpqNumber = CheckRevision.ExtractMPQNumber(filename);
		int CheckRevisionChecksum = CheckRevision.DoCheckRevision(valuestring, gamefiles, CheckRevision.ExtractMPQNumber(filename));
		_EXEVersion = CheckRevision.GetExeInfo(gamefiles[0], out var _);
		_EXEHash = CheckRevisionChecksum;
		_KeyLength1 = classic.Key.Length;
		_CDKeyProductValue1 = classic.Product;
		_CDKeyPublicValue1 = classic.Value1;
		_HashedKeyData1 = classic.GetHash(clienttoken, servertoken);
		_KeyLength2 = lod.Key.Length;
		_CDKeyProductValue2 = lod.Product;
		_CDKeyPublicValue2 = lod.Value1;
		_HashedKeyData2 = lod.GetHash(clienttoken, servertoken);
		_CDKeyOwnerName = ownername;
		Data = Build();
	}

	public byte[] Build()
	{
		BncsPacket myPacket = new BncsPacket(81);
		myPacket.InsertInt32((int)_ClientToken);
		myPacket.InsertInt32(_EXEVersion);
		myPacket.InsertInt32(_EXEHash);
		myPacket.InsertInt32(_NumberOfKeys);
		myPacket.InsertInt32(_UsingSpawnBoolean);
		myPacket.InsertInt32(_KeyLength1);
		myPacket.InsertInt32(_CDKeyProductValue1);
		myPacket.InsertInt32(_CDKeyPublicValue1);
		myPacket.InsertInt32(_Unknown1);
		myPacket.InsertByteArray(_HashedKeyData1);
		myPacket.InsertInt32(_KeyLength2);
		myPacket.InsertInt32(_CDKeyProductValue2);
		myPacket.InsertInt32(_CDKeyPublicValue2);
		myPacket.InsertInt32(_Unknown2);
		myPacket.InsertByteArray(_HashedKeyData2);
		myPacket.InsertCString(_EXEInfo);
		myPacket.InsertCString(_CDKeyOwnerName);
		return myPacket.GetData();
	}
}
