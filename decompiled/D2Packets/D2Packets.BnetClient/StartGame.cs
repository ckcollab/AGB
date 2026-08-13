using System;
using ETUtils;

namespace D2Packets.BnetClient;

/// <summary>
/// Bnet Client Packet 0x1C - Start Game
/// <para>Notifies Battle.net a game has been started so it can advertise it.</para>
/// </summary>
public class StartGame : BCPacket
{
	protected StartGameFlags flags;

	protected string name;

	protected string password;

	protected string statString;

	public StartGameFlags Flags => flags;

	public string Name => name;

	public string Password => password;

	/// <summary>
	/// Doesn't seem to be used by Diablo II...
	/// </summary>
	public string StatString => statString;

	/// <summary>
	/// Doesn't seem to be used by Diablo II...
	/// </summary>
	public string Unknown7 => ByteConverter.ToHexString(data, 7, 16);

	public StartGame(byte[] data)
		: base(data)
	{
		flags = (StartGameFlags)BitConverter.ToUInt32(data, 4);
		name = ByteConverter.GetNullString(data, 24);
		if ((flags & StartGameFlags.Private) == StartGameFlags.Private)
		{
			password = ByteConverter.GetNullString(data, 25 + name.Length);
		}
		int offset = 26 + name.Length + ((password != null) ? password.Length : 0);
		if (data.Length > offset + 1)
		{
			statString = ByteConverter.GetNullString(data, offset);
		}
	}
}
