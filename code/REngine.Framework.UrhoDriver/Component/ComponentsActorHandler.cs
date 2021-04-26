using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class ComponentsActorHandler
	{
		private Actor _actor;
		private IComponentCollection _componentCollection;
		private IDictionary<Type, IList<IComponent>> _components = new Dictionary<Type, IList<IComponent>>();

		public ComponentsActorHandler(Actor actor, IComponentCollection collection)
		{
			_componentCollection = collection;
			_actor = actor;
		}


	}
}
