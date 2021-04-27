using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class NativeComponentAttribute : Attribute
	{
		public uint HashCode { get; set; }
		public string Name { get; set; }
		public NativeComponentAttribute(string name)
		{
			Name = name;
			HashCode = HashUtils.SDBM(name);
		}
	}
}
