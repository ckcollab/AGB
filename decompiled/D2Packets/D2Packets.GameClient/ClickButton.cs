using System;
using D2Data;

namespace D2Packets.GameClient;

/// <summary>
/// Game Client Packet 0x4F - Click Button
/// <para>Click on a UI button.</para>
/// </summary>
public class ClickButton : GCPacket
{
	protected GameButton button;

	protected ushort complement;

	public GameButton Button => button;

	public ushort Complement => complement;

	public ClickButton(byte[] data)
		: base(data)
	{
		button = (GameButton)BitConverter.ToUInt32(data, 1);
		complement = BitConverter.ToUInt16(data, 5);
	}

	public ClickButton(GameButton button, ushort complement)
		: base(Build(button, complement))
	{
		this.button = button;
		this.complement = complement;
	}

	public static byte[] Build(GameButton button, ushort complement)
	{
		return new byte[7]
		{
			79,
			(byte)button,
			(byte)((uint)button >> 8),
			(byte)((uint)button >> 16),
			(byte)((uint)button >> 24),
			(byte)complement,
			(byte)(complement >> 8)
		};
	}
}
