using System;

namespace AGB;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class Serialize : Attribute
{
}
