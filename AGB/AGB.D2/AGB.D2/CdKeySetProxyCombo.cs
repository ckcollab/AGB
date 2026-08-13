using AGB.Net;

namespace AGB.D2;

public class CdKeySetProxyCombo
{
	public CdKeySet CdKeySet;

	public Proxy Proxy;

	public CdKeySetProxyCombo()
	{
	}

	public CdKeySetProxyCombo(CdKeySet cdKeySet, Proxy proxy)
	{
		CdKeySet = cdKeySet;
		Proxy = proxy;
	}
}
