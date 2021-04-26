using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
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
			IActor actor = null;
			IntPtr pinnedPtr = CoreInternals.Object_GetManagedRefPtr(handle);
			
			if(pinnedPtr.Equals(IntPtr.Zero))
			{
				Handler handler = pinnedPtr;
				actor = Wrap(handler);
				SetObjectOnHandle(handler, actor);
				handler.OnDestroy += HandlePtrDestroy;
			} else
			{
				GCHandle gcHandle = GCHandle.FromIntPtr(pinnedPtr);
				actor = (IActor)gcHandle.Target;
			}

			return actor;
		}

		public IActor GetParent(IActor actor)
		{
			Handler handler = ActorInternals.Node_GetParent(GetPointerFromObj(actor));
			return Wrap(handler);
		}

		public IWorld GetWorld(IActor actor)
		{
			Handler handler = ActorInternals.Node_GetScene(GetPointerFromObj(actor));
			return RootDriver.WorldDriver.Wrap(handler);
		}

		public void SetParent(IActor actor, IActor target)
		{
			ActorInternals.Node_SetParent(
				GetPointerFromObj(actor),
				GetPointerFromObj(target)
			);
		}

		public int GetId(IActor src)
		{
			return (int)ActorInternals.Node_GetID(GetPointerFromObj(src));
		}
	}
}
