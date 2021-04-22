using REngine.Framework.Reflection;
using REngine.Framework.Resources;
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

		public Type ObjectType
		{
			get => _interface is null ? _type : _interface;
		}

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

		public IResource Instantiate()
		{
			if(_func != null)
			{
				return (IResource)_func();
			} else
			{
				TypeExtractor extractor = new TypeExtractor(_type).CollectOnlyCtor();
				return extractor.Instantiate<IResource>();
			}
		}
	}
}
