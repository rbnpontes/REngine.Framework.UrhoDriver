using REngine.Framework.Components;
using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class NativeComponentAttribute : BaseComponentAttribute
	{
		public uint HashCode { get; set; }
		public string Name { get; set; }
		public NativeComponentAttribute(string name, Type @interface) : base(@interface)
		{
			Name = name;
			HashCode = HashUtils.SDBM(name);
		}
	}
}
