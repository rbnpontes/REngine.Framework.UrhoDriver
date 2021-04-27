using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	public struct ComponentInfo
	{
		public bool IsNative;
		public uint HashCode;
		public Type Type;
		public Type ImplType;
		public TypeExtractor Ctor;
	}
}
