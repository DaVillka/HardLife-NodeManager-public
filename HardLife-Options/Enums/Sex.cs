using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
