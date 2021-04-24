using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System.Collections.Generic;

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
			Actor actor = new Actor(handle as Handler, RootDriver);
			return actor;
		}

		public IActor ListGetterCallback(Handler handle)
		{
			return Wrap(handle);
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
	}
}
