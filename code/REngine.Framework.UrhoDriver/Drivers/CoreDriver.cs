using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal sealed class CoreDriver : BaseDriver, ICoreDriver
	{
		public CoreDriver(RootDriver driver) : base(driver)
		{
		}

		public void Destroy(IHandle handle)
		{
			throw new NotImplementedException();
		}

		public void Dump()
		{
			throw new NotImplementedException();
		}

		public bool HasDestroyed(IHandle handle)
		{
			throw new NotImplementedException();
		}
	}
}
