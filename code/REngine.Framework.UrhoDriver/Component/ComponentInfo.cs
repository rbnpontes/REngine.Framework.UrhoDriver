using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal struct ComponentInfo
	{
		public bool IsNative;
		public Type Type;
		public TypeExtractor Ctor;
	}
}
