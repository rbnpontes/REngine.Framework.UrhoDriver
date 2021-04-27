using REngine.Framework.Components;
using REngine.Framework.Reflection;
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
		private ComponentCollection _componentCollection;
		private IDictionary<Type, IList<IComponent>> _components = new Dictionary<Type, IList<IComponent>>();

		public ComponentsActorHandler(Actor actor, IComponentCollection collection)
		{
			_componentCollection = collection as ComponentCollection;
			_actor = actor;
		}

		private IList<IComponent> GetOrCreateList(Type type)
		{
			IList<IComponent> components;

			if (_components.TryGetValue(type, out components))
				return components;
			_components[type] = components = new List<IComponent>();

			return components;
		}

		public IComponent Create(Type type)
		{
			IList<IComponent> components = GetOrCreateList(type);
			TypeExtractor componentCtor = _componentCollection.GetComponentCtor(type);

			if (componentCtor is null)
				throw new ArgumentException("Incompatible Component Type, current type is not registered!");

			IComponent component = componentCtor.Instantiate<IComponent>();
			component.Owner = _actor;
			component.World = _actor.World;

			components.Add(component);
			return component;
		}

		public IList<IComponent> GetComponents(Type type)
		{
			IList<IComponent> components;

			if (_components.TryGetValue(type, out components))
				return components;
			return new List<IComponent>();
		}

		public IList<IComponent> GetAllComponents()
		{
			List<IComponent> components = new List<IComponent>();

			foreach(var key in _components)
			{
				components = components.Concat(key.Value).ToList();
			}

			return components;
		}

		public void RemoveComponent(IComponent component)
		{
			IList<IComponent> components = GetComponents(component.GetType());

			if (components.Count == 0)
				return;

			component.OnRemove();
			components.Remove(component);
		}

		public void RemoveComponentByType(Type type)
		{
			IList<IComponent> components = GetComponents(type);

			if (components.Count == 0)
				return;

			components.FirstOrDefault().OnRemove();
			components.RemoveAt(0);
		}

		public void RemoveComponents(Type type)
		{
			IList<IComponent> components = GetComponents(type);

			if (components.Count == 0)
				return;

			components.ToList().ForEach(x => x.OnRemove());
			components.Clear();
		}
	}
}
