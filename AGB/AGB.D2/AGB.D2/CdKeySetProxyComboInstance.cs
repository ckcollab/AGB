using System;
using System.Collections.Generic;

namespace AGB.D2;

internal class CdKeySetProxyComboInstance : CdKeySetProxyCombo
{
	public TimeSpan IgnoreLength;

	public DateTime Released;

	public List<LoggedGame> Games = new List<LoggedGame>();

	public CdKeySetInstance CdKeySetInstance;

	public ProxyInstance ProxyInstance;

	public CdKeySetProxyComboInstance()
	{
	}

	public CdKeySetProxyComboInstance(CdKeySetInstance cdKeySetInstance, ProxyInstance proxyInstance)
		: base(cdKeySetInstance, proxyInstance)
	{
		CdKeySetInstance = cdKeySetInstance;
		ProxyInstance = proxyInstance;
	}
}
