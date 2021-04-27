using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	public sealed class ComponentScope
	{
		private ComponentCollection _collection;
		private Actor _owner;
		private RootDriver Driver { get => _owner.Driver; }

		private IDictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
		private IComponent[] _cachedComponents = new IComponent[0];
		internal ComponentScope(Actor actor, ComponentCollection collection)
		{
			_owner = actor;
			_collection = collection;
		}

		public void Update()
		{

		}

		public IComponent[] GetComponents()
		{
			return _cachedComponents;
		}
		
		private void ThrowUnregisteredComponentException(Type type)
		{
			throw new ArgumentException($"There's no Component registered with this type name: {type.Name}.", "type");
		}

		private bool HasNativeComponent(ComponentInfo componentInfo)
		{
			return Driver.ActorDriver.HasComponent(_owner, componentInfo.ImplType);
		} 

		private bool HasManagedComponent(ComponentInfo componentInfo)
		{
			return _components.ContainsKey(componentInfo.Type);
		}

		private void ThrowComponentExistException(Type type)
		{
			throw new InvalidOperationException($"Component {type.Name} has been added to Actor.");
		}

		private void ThrowComponentNotExistException(Type type)
		{
			throw new NullReferenceException($"There's no Component for this type name: {type.Name}");
		}

		private IComponent CreateNative(ComponentInfo componentInfo)
		{
			if (HasNativeComponent(componentInfo))
				ThrowComponentExistException(componentInfo.Type);

			IComponent component = Driver.ActorDriver.CreateComponent(_owner, componentInfo.ImplType);
			return component;
		}

		private IComponent CreateManaged(ComponentInfo componentInfo)
		{
			if (HasManagedComponent(componentInfo))
				ThrowComponentExistException(componentInfo.Type);

			IComponent component = componentInfo.Ctor.Instantiate<IComponent>();
			component.Owner = _owner;
			component.World = _owner.World;
			component.OnAwake();
			component.OnStart();

			_components[componentInfo.Type] = component;
			return component;
		}

		public IComponent Create(Type type)
		{
			ComponentInfo cp;

			if (!_collection.TryGetComponentInfo(type, out cp))
				ThrowUnregisteredComponentException(type);

			return cp.IsNative ? CreateNative(cp) : CreateManaged(cp);
		}

		private IComponent GetNativeComponent(ComponentInfo componentInfo)
		{
			if (!HasNativeComponent(componentInfo))
				ThrowComponentNotExistException(componentInfo.Type);

			IComponent component = Driver.ActorDriver.GetComponent(_owner, componentInfo.ImplType);
			return component;
		}
		
		private IComponent GetManagedComponent(ComponentInfo componentInfo)
		{
			if (!HasManagedComponent(componentInfo))
				ThrowComponentNotExistException(componentInfo.Type);

			return _components[componentInfo.Type];
		}

		public IComponent GetComponent(Type type)
		{
			ComponentInfo cp;
			
			if(!_collection.TryGetComponentInfo(type, out cp))
				ThrowUnregisteredComponentException(type);

			return cp.IsNative ? GetNativeComponent(cp) : GetManagedComponent(cp);
		}

		public bool HasComponent(Type type)
		{
			ComponentInfo cp;
			if (!_collection.TryGetComponentInfo(type, out cp))
				ThrowUnregisteredComponentException(type);

			return cp.IsNative ? HasNativeComponent(cp) : HasManagedComponent(cp);
		}

		private void RemoveNativeComponent(ComponentInfo cp)
		{
			if (!HasNativeComponent(cp))
				return;
			Driver.ActorDriver.RemoveComponent(_owner, cp.ImplType);
		}

		private void RemoveManagedComponent(ComponentInfo cp)
		{
			if (!HasManagedComponent(cp))
				return;
			IComponent component = _components[cp.Type];
			_components.Remove(cp.Type);

			component.OnDestroy(); // Call on Destroy Event
		}

		public void RemoveComponent(Type type)
		{
			ComponentInfo cp;
			if (!_collection.TryGetComponentInfo(type, out cp))
				ThrowUnregisteredComponentException(type);

			if (cp.IsNative)
				RemoveNativeComponent(cp);
			else
				RemoveManagedComponent(cp);
		}
	}
}
