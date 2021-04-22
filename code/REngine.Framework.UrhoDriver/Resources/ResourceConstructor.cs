using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	internal class ResourceConstructor
	{
		private Type _type;
		private Type _interface;
		private Func<object> _func;

		public ResourceConstructor(Type type, Type @interface = null)
		{
			_type = type;
			_interface = @interface;
		}

		public ResourceConstructor(Type @interface, Func<object> func)
		{
			_interface = @interface;
			_func = func;
		}

		public object Instantiate()
		{
			if(_func != null)
			{
				return _func();
			} else
			{
				TypeExtractor extractor = new TypeExtractor(_type).CollectOnlyCtor();
				return extractor.Instantiate();
			}
		}
	}
}
