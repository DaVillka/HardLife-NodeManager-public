using System;

namespace HardLife_Options.Enums
{
	[Flags]
	public enum Sex
	{
		None = 0,
		Male = 1 << 0,
		Female = 1 << 1,
	}
}
