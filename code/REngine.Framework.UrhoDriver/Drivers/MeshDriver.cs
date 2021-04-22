using REngine.Framework.Drivers;
using REngine.Framework.Resources;
using REngine.Framework.UrhoDriver.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class MeshDriver : BaseDriver, IWrapper<IMesh>
	{
		public MeshDriver(RootDriver driver) : base(driver)
		{
		}

		public IMesh Wrap(IHandle handle)
		{
			var mesh = new Mesh();
			mesh.Handle = handle as Handler;
			mesh.RootDriver = RootDriver;
			return mesh;
		}
	}
}
