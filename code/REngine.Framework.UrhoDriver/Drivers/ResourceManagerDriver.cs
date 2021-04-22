using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class ResourceManagerDriver : BaseDriver
	{
		public ResourceManagerDriver(RootDriver driver) : base(driver)
		{
		}

		public Handler GetResourceCacheHandle()
		{
			Handler handler = ResourceCacheInternals.ResourceCache_Get(RootDriver.ContextPtr);
			return handler;
		}
	}
}
