using REngine.Framework.Components;
using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Component;
using REngine.Framework.UrhoDriver.Internals;
using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class ActorDriver : BaseDriver, IActorDriver
	{
		public ActorDriver(RootDriver driver) : base(driver)
		{
		}

		public void AddChild(IActor src, IActor handle)
		{
			ActorInternals.Node_AddChild(
				GetPointerFromObj(src),
				GetPointerFromObj(handle)
			);
		}

		public IActor Clone(IActor actor)
		{
			Handler handler = ActorInternals.Node_Clone(GetPointerFromObj(actor));
			return Wrap(handler);
		}

		public void Destroy(IActor handle)
		{
			ActorInternals.Node_Destroy(GetPointerFromObj(handle));
		}

		public IReadOnlyList<IActor> GetChildren(IActor handle)
		{
			Handler list = ActorInternals.Node_GetChildren(GetPointerFromObj(handle));
			return new InternalList<IActor>(list, ListGetterCallback);
		}

		public string GetName(IActor handle)
		{
			return ActorInternals.Node_GetName(GetPointerFromObj(handle));
		}

		public void RemoveChild(IActor src, IActor handle)
		{
			ActorInternals.Node_RemoveChild(
				GetPointerFromObj(src),
				GetPointerFromObj(handle)
			);
		}

		public void SetName(IActor handle, string value)
		{
			ActorInternals.Node_SetName(GetPointerFromObj(handle), value);
		}

		public void Detach(IActor actor)
		{
			ActorInternals.Node_Detach(GetPointerFromObj(actor));
		}

		public IActor Wrap(IHandle handle)
		{
			Handler handler = handle as Handler;
			handler.TypeName = nameof(Actor);
			Actor actor = new Actor(handler, RootDriver);
			return actor;
		}

		public IActor ListGetterCallback(IntPtr handle)
		{
			return TryBindReferenceHolder(handle, Wrap);
		}

		public IActor GetParent(IActor actor)
		{
			Handler handler = ActorInternals.Node_GetParent(GetPointerFromObj(actor));
			return Wrap(handler);
		}

		public IWorld GetWorld(IActor actor)
		{
			IntPtr handler = ActorInternals.Node_GetScene(GetPointerFromObj(actor));
			return TryBindReferenceHolder(handler, (RootDriver.WorldDriver as WorldDriver).Wrap);
		}

		public void SetParent(IActor actor, IActor target)
		{
			if (actor is null)
				return;
			ActorInternals.Node_SetParent(
				GetPointerFromObj(actor),
				GetPointerFromObj(target)
			);
		}

		public int GetId(IActor src)
		{
			if (src is null)
				return -1;
			return (int)ActorInternals.Node_GetID(GetPointerFromObj(src));
		}

		public bool HasComponent(IActor actor, Type type)
		{
			return ActorInternals.Node_HasComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
		}

		public IComponent CreateComponent(IActor actor, Type type)
		{
			Handler handler = ActorInternals.Node_CreateComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
			IComponent component = RootDriver.ComponentDriver.Wrap(type, handler);

			handler.OnAdd += GetPtrAddDelegate(component);
			handler.OnRelease += GetPtrReleaseDelegate(component);
			handler.OnDestroy += HandlePtrDestroy;

			return component;
		}

		public IComponent GetComponent(IActor actor, Type type)
		{
			IntPtr componentPtr = ActorInternals.Node_GetComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));

			return (RootDriver.ComponentDriver as ComponentDriver).ListGetterCallback(componentPtr);
		}

		public IReadOnlyList<IComponent> GetAllComponents(IActor actor)
		{
			Handler handler = ActorInternals.Node_GetAllComponents(GetPointerFromObj(actor));
			return new InternalList<IComponent>(handler, (RootDriver.ComponentDriver as ComponentDriver).ListGetterCallback);
		}

		public void RemoveComponent(IActor actor, Type type)
		{
			ActorInternals.Node_RemoveComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
		}
		
		private uint GetHashCodeFromType(Type type)
		{
			NativeComponentAttribute attribute = type.GetCustomAttribute<NativeComponentAttribute>();

			return attribute?.HashCode ?? HashUtils.SDBM(type.Name); // if type is not a component, try to create a hashcode by a name type
		}

		public void SetEnabled(IActor src, bool value)
		{
			ActorInternals.Node_SetEnabled(GetPointerFromObj(src), value);
		}

		public bool IsEnabled(IActor src)
		{
			return ActorInternals.Node_IsEnabled(GetPointerFromObj(src));
		}
	}
}
