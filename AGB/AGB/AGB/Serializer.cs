using System.Reflection;

namespace AGB;

public class Serializer
{
	public static byte[] Serialize(object o)
	{
		MemberInfo membInfo = o.GetType();
		object[] attributes = membInfo.GetCustomAttributes(typeof(Serialize), inherit: true);
		if (attributes.GetLength(0) != 0)
		{
			Serialize pr = (Serialize)attributes[0];
		}
		return null;
	}
}
