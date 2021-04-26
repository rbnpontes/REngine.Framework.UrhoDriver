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

		private ComponentsActorHandler _componentsHandler;
	
		public Actor(Handler handler, RootDriver driver) : base(handler, driver) {
			_componentsHandler = new ComponentsActorHandler(this, driver.ComponentCollection);
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
			return (T)_componentsHandler.Create(typeof(T));
		}

		public IComponent CreateComponent(Type component)
		{
			return _componentsHandler.Create(component);
		}

		public T GetComponent<T>()
		{
			var component = _componentsHandler.GetComponents(typeof(T)).FirstOrDefault();
			if (component is null)
				return default(T);
			return (T)component;
		}

		public IComponent GetComponent(Type componentType)
		{
			return _componentsHandler.GetComponents(componentType).FirstOrDefault();
		}

		public IReadOnlyList<T> GetComponents<T>()
		{
			return (_componentsHandler.GetComponents(typeof(T)) as List<IComponent>).Select(x => (T)x).ToList().AsReadOnly();
		}

		public IReadOnlyList<IComponent> GetComponents(Type component)
		{
			return (_componentsHandler.GetComponents(component) as List<IComponent>).AsReadOnly();
		}

		public IReadOnlyList<IComponent> GetAllComponents()
		{
			return (_componentsHandler.GetAllComponents() as List<IComponent>).AsReadOnly();
		}

		public IActor RemoveComponent<T>()
		{
			return RemoveComponent(typeof(T));
		}

		public IActor RemoveComponent(Type type)
		{
			_componentsHandler.RemoveComponentByType(type);
			return this;
		}

		public IActor RemoveComponents<T>()
		{
			return RemoveComponents(typeof(T));
		}

		public IActor RemoveComponents(Type type)
		{
			_componentsHandler.RemoveComponents(type);
			return this;
		}
	}
}
