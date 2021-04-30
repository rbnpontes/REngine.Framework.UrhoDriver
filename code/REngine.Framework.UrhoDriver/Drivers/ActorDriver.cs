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
			if (HandleHasDestroyed(src.Handle) || HandleHasDestroyed(handle.Handle))
				return;
			ActorInternals.Node_AddChild(
				GetPointerFromObj(src),
				GetPointerFromObj(handle)
			);
		}

		public IActor Clone(IActor actor)
		{
			if (HandleHasDestroyed(actor.Handle))
				return null;
			Handler handler = ActorInternals.Node_Clone(GetPointerFromObj(actor));
			return Wrap(handler);
		}

		public void Destroy(IActor handle)
		{
			if (HandleHasDestroyed(handle.Handle))
				return;
			ActorInternals.Node_Destroy(GetPointerFromObj(handle));
		}

		public IReadOnlyList<IActor> GetChildren(IActor handle)
		{
			if (HandleHasDestroyed(handle.Handle))
				return Constants.EmptyActorList;
			Handler list = ActorInternals.Node_GetChildren(GetPointerFromObj(handle));
			return new InternalList<IActor>(list, ListGetterCallback);
		}

		public string GetName(IActor handle)
		{
			if (HandleHasDestroyed(handle.Handle))
				return string.Empty;
			return ActorInternals.Node_GetName(GetPointerFromObj(handle));
		}

		public void RemoveChild(IActor src, IActor handle)
		{
			if (HandleHasDestroyed(src.Handle) || HandleHasDestroyed(handle.Handle))
				return;
			ActorInternals.Node_RemoveChild(
				GetPointerFromObj(src),
				GetPointerFromObj(handle)
			);
		}

		public void SetName(IActor handle, string value)
		{
			if (HandleHasDestroyed(handle.Handle))
				return;
			ActorInternals.Node_SetName(GetPointerFromObj(handle), value);
		}

		public void Detach(IActor actor)
		{
			if (HandleHasDestroyed(actor.Handle))
				return;
			ActorInternals.Node_Detach(GetPointerFromObj(actor));
		}

		public IActor Wrap(IHandle handle)
		{
			if (HandleHasDestroyed(handle))
				return null;
			Handler handler = handle as Handler;
			handler.TypeName = nameof(Actor);
			Actor actor = new Actor(handler, RootDriver);
			return actor;
		}

		public IActor ListGetterCallback(IntPtr handle)
		{
			if (handle.Equals(IntPtr.Zero))
				return null;
			return TryBindReferenceHolder(handle, Wrap);
		}

		public IActor GetParent(IActor actor)
		{
			if (HandleHasDestroyed(actor.Handle))
				return null;
			IntPtr ptr = ActorInternals.Node_GetParent(GetPointerFromObj(actor));
			if (ptr.Equals(IntPtr.Zero))
				return null;
			return TryBindReferenceHolder(ptr, Wrap);
		}

		public IWorld GetWorld(IActor actor)
		{
			if (HandleHasDestroyed(actor.Handle))
				return null;
			IntPtr handler = ActorInternals.Node_GetScene(GetPointerFromObj(actor));
			if (handler.Equals(IntPtr.Zero))
				return null;
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
			if (HandleHasDestroyed(src.Handle))
				return -1;
			return (int)ActorInternals.Node_GetID(GetPointerFromObj(src));
		}

		public bool HasComponent(IActor actor, Type type)
		{
			if (HandleHasDestroyed(actor.Handle))
				return false;
			return ActorInternals.Node_HasComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
		}

		public IComponent CreateComponent(IActor actor, Type type)
		{
			if (HandleHasDestroyed(actor.Handle))
				return null;

			IntPtr componentPtr = ActorInternals.Node_CreateComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
			return TryBindReferenceHolder(componentPtr, (handle) => RootDriver.ComponentDriver.Wrap(type, handle));
		}

		public IComponent GetComponent(IActor actor, Type type)
		{
			if (HandleHasDestroyed(actor.Handle))
				return null;
			IntPtr componentPtr = ActorInternals.Node_GetComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
			return (RootDriver.ComponentDriver as ComponentDriver).ListGetterCallback(componentPtr);
		}

		public IReadOnlyList<IComponent> GetAllComponents(IActor actor)
		{
			if (HandleHasDestroyed(actor.Handle))
				return Constants.EmptyComponentList;

			Handler handler = ActorInternals.Node_GetAllComponents(GetPointerFromObj(actor));
			return new InternalList<IComponent>(handler, (RootDriver.ComponentDriver as ComponentDriver).ListGetterCallback);
		}

		public void RemoveComponent(IActor actor, Type type)
		{
			if (HandleHasDestroyed(actor.Handle))
				return;
			ActorInternals.Node_RemoveComponent(GetPointerFromObj(actor), GetHashCodeFromType(type));
		}
		
		private uint GetHashCodeFromType(Type type)
		{
			NativeComponentAttribute attribute = type.GetCustomAttribute<NativeComponentAttribute>();

			return attribute?.HashCode ?? HashUtils.SDBM(type.Name); // if type is not a component, try to create a hashcode by a name type
		}

		public void SetEnabled(IActor src, bool value)
		{
			if (HandleHasDestroyed(src.Handle))
				return;
			ActorInternals.Node_SetEnabled(GetPointerFromObj(src), value);
		}

		public bool IsEnabled(IActor src)
		{
			if (HandleHasDestroyed(src.Handle))
				return false;
			return ActorInternals.Node_IsEnabled(GetPointerFromObj(src));
		}
	}
}
