namespace AGB.Net;

public delegate void PacketEvent(byte[] data, int bytesTransferred);
public delegate void PacketEvent<T>(object sender, T p);
