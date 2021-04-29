using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class WorldDriver : BaseDriver, IWorldDriver
	{
		public WorldDriver(RootDriver driver) : base(driver)
		{
		}

		public void Clear(IWorld handle)
		{
			WorldInternals.Scene_Clear(GetPointerFromObj(handle));
		}

		public IWorld Clone(IWorld world)
		{
			throw new NotImplementedException();
		}

		public IWorld Create(Root root)
		{
			IntPtr worldPtr = WorldInternals.Scene_Create(RootDriver.ContextPtr);
			return TryBindReferenceHolder(worldPtr, Wrap);
		}

		public IActor CreateActor(IWorld world)
		{
			IntPtr actorPtr = WorldInternals.Scene_CreateChild(GetPointerFromObj(world));
			return TryBindReferenceHolder(actorPtr, (RootDriver.ActorDriver as ActorDriver).Wrap);
		}

		public IReadOnlyList<IActor> GetActors(IWorld world)
		{
			// Urho3D Scene inherits Node Type
			Handler handler = ActorInternals.Node_GetChildren(GetPointerFromObj(world));
			return new InternalList<IActor>(handler, (RootDriver.ActorDriver as ActorDriver).ListGetterCallback);
		}

		public IWorld Wrap(IHandle handle)
		{
			return new World(handle as Handler, RootDriver);
		}
	}
}
