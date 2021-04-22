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
		public Handler Handle { get; internal set; } = Handler.Zero;
		public RootDriver RootDriver { get; internal set; }
		public NativeResource() : base(true)
		{
		}
		public abstract void LoadResource(Handler resourceCache, string name);
	}
}
