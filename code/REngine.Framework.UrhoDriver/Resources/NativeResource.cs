using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	internal abstract class NativeResource : BaseResource
	{
		public Handler Handle { get; private set; }
		public RootDriver RootDriver { get; private set; }
		public NativeResource(Handler handle, RootDriver driver) : base(true)
		{
			Handle = handle;
			RootDriver = driver;
		}
	}
}
