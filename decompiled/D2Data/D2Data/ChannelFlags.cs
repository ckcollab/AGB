using System;

namespace D2Data;

[Flags]
public enum ChannelFlags
{
	PublicChannel = 1,
	Moderated = 2,
	Restricted = 4,
	Silent = 8,
	System = 0x10,
	ProductSpecific = 0x20,
	GloballyAccessible = 0x1000
}
