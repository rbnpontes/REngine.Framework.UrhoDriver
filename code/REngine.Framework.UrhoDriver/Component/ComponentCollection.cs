using REngine.Framework.Components;
using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class ComponentCollection : IComponentCollection
	{
		private IDictionary<Type, TypeExtractor> _components = new Dictionary<Type, TypeExtractor>();

		private void ValidateType(Type type)
		{
			if (_components.ContainsKey(type))
				throw new ArgumentException("This component has been already registered!");
		}

		public IComponentCollection Add<Type>()
		{
			return Add(typeof(Type));
		}

		public IComponentCollection Add<Type, Interface>()
		{
			return Add(typeof(Type), typeof(Interface));
		}

		public IComponentCollection Add(Type type, Type @interface = null)
		{
			ValidateType(@interface ?? type);
			TypeExtractor typeExtractor = new TypeExtractor(type);
			_components[@interface ?? type] = typeExtractor;

			return this;
		}

		public IComponentCollection Create<T>(IActor actor)
		{
			return Create(actor, typeof(T));
		}

		public IComponentCollection Create(IActor actor, Type type)
		{
			actor.CreateComponent(type);
			return this;
		}

	}
}
