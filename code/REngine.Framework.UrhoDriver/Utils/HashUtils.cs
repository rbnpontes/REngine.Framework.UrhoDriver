using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Utils
{
	public static class HashUtils
	{
		public static uint SDBM(string str)
		{
			uint hash = 0u;
			for(int i = 0; i < str.Length; i++)
			{
				char charItem = str[i];

				hash = charItem + (hash << 6) + (hash << 16) - hash;
			}

			return hash;
		}
	}
}
