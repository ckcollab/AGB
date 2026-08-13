using D2Packets.D2Packets;

namespace AGB.D2.Net;

public delegate void UnknownPacketEventHandler(PacketOrigin origin, byte[] data, int bytesTransferred);
