using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	public sealed class RootDriver : IRootDriver
	{
		public IActorDriver ActorDriver { get; set; }

		public IWorldDriver WorldDriver { get; set; }

		public ICoreDriver CoreDriver { get; set; }

		public IEngineDriver EngineDriver { get; set; }

		public RootDriver()
		{
			ActorDriver = new ActorDriver(this);
			WorldDriver = new WorldDriver(this);
			CoreDriver = new CoreDriver(this);
			EngineDriver = new EngineDriver(this);
		}
	}
}
