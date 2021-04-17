using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
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
			throw new NotImplementedException();
		}

		public IWorld Clone(IWorld world)
		{
			throw new NotImplementedException();
		}

		public IWorld Create(Root root)
		{
			throw new NotImplementedException();
		}

		public IActor CreateActor(IWorld world)
		{
			throw new NotImplementedException();
		}

		public INativeList GetActors(IWorld world)
		{
			throw new NotImplementedException();
		}

		public IWorld Wrap(IHandle handle)
		{
			throw new NotImplementedException();
		}
	}
}
