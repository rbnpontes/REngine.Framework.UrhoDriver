using REngine.Framework.Components;
using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class ComponentCollection
	{
		private IDictionary<uint, Type> _nativeComponents = new Dictionary<uint, Type>();
		private IDictionary<Type, TypeExtractor> _components = new Dictionary<Type, TypeExtractor>();

		private void ValidateType(Type type)
		{
			if (_components.ContainsKey(type))
				throw new ArgumentException("This component has been already registered!");
			if (!type.IsAssignableFrom(typeof(IComponent)))
				throw new ArgumentException("Current Type not Implements IComponent interface");
		}

		public void Collect()
		{

		}

		public TypeExtractor GetComponentCtor(Type type)
		{
			TypeExtractor result;
			_components.TryGetValue(type, out result);
			return result;
		}	
	}
}
