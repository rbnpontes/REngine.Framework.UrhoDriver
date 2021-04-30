using REngine.Framework.Components;
using REngine.Framework.UrhoDriver.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class Actor : NativeObject, IActor
	{
		public bool Enabled { get => Driver.ActorDriver.IsEnabled(this); set => Driver.ActorDriver.SetEnabled(this, value); }
		public int Id { get => Driver.ActorDriver.GetId(this); }
		public string Name { 
			get => Driver.ActorDriver.GetName(this); 
			set => Driver.ActorDriver.SetName(this, value); 
		}
		public IWorld World { 
			get => Driver.ActorDriver.GetWorld(this); 
			set => throw new NotImplementedException(); 
		}
		public IActor Parent {
			get => Driver.ActorDriver.GetParent(this);
			set => Driver.ActorDriver.SetParent(this, value); 
		}

		public IReadOnlyList<IActor> Children
		{
			get
			{
				return Driver.ActorDriver.GetChildren(this);
			}
		}

		public ComponentScope ComponentScope { get; private set; }
		public Actor(Handler handler, RootDriver driver) : base(handler, driver) {
			ComponentScope = driver.ComponentCollection.BuildComponentScope(this);
		}

		public IActor AddChild(IActor actor)
		{
			Driver.ActorDriver.AddChild(this, actor);
			return this;
		}

		public object Clone()
		{
			return Driver.ActorDriver.Clone(this);
		}

		public void Destroy()
		{
			Driver.ActorDriver.Destroy(this);
		}

		public IActor RemoveChild(IActor actor)
		{
			Driver.ActorDriver.RemoveChild(this, actor);
			return this;
		}

		public IActor Detach()
		{
			Driver.ActorDriver.Detach(this);
			return this;
		}

		public T CreateComponent<T>()
		{
			return (T)CreateComponent(typeof(T));
		}

		public IComponent CreateComponent(Type component)
		{
			return ComponentScope.Create(component);
		}

		public T GetComponent<T>()
		{
			var component = GetComponent(typeof(T));
			if (component is null)
				return default(T);
			return (T)component;
		}

		public IComponent GetComponent(Type componentType)
		{
			if (IsDestroyed)
				return null;
			return ComponentScope.GetComponent(componentType);
		}

		public IReadOnlyList<IComponent> GetAllComponents()
		{
			if (IsDestroyed)
				return Constants.EmptyComponentList;
			return ComponentScope.GetComponents().ToList().AsReadOnly();
		}

		public IActor RemoveComponent<T>()
		{
			return RemoveComponent(typeof(T));
		}

		public IActor RemoveComponent(Type type)
		{
			ComponentScope.RemoveComponent(type);
			return this;
		}

		public bool HasComponent<T>()
		{
			return HasComponent(typeof(T));
		}

		public bool HasComponent(Type type)
		{
			return ComponentScope.HasComponent(type);
		}
	}
}
