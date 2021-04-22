using REngine.Framework.Components;
using REngine.Framework.Drivers;
using REngine.Framework.Resources;
using REngine.Framework.UrhoDriver.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using REngine.Framework.UrhoDriver.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	public sealed class RootDriver : IRootDriver
	{
		public IActorDriver ActorDriver { get; private set; }

		public IWorldDriver WorldDriver { get; private set; }

		public ICoreDriver CoreDriver { get; private set; }

		public IEngineDriver EngineDriver { get; private set; }

		#region INTERNAL DRIVERS
		internal ResourceManagerDriver ResourceManagerDriver { get; private set; }
		internal MeshDriver MeshDriver { get; private set; }
		#endregion

		public IComponentCollection ComponentCollection => throw new NotImplementedException();

		public IResourcesCollection ResourcesCollection { get; private set; }

		public IntPtr ContextPtr { get; private set; } = IntPtr.Zero;

		/// <summary>
		/// This will used for block any calls outside from Main Thread
		/// </summary>
		public Thread CurrentThread { get; private set; }

		public RootDriver()
		{
			CurrentThread = Thread.CurrentThread;

			ContextPtr = CoreInternals.Context_New();

			ActorDriver = new ActorDriver(this);
			WorldDriver = new WorldDriver(this);
			CoreDriver = new CoreDriver(this);
			EngineDriver = new EngineDriver(this);

			ResourcesCollection = new ResourceCollection(this);
			// Setup Internal Drivers
			ResourceManagerDriver = new ResourceManagerDriver(this);
			MeshDriver = new MeshDriver(this);

		}

		public void Dispose()
		{
			CoreInternals.Object_Free(ContextPtr);
			ContextPtr = IntPtr.Zero;
		}
	}
}
