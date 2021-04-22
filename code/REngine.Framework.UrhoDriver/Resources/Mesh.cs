using REngine.Framework.Drivers;
using REngine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	internal class Mesh : NativeResource, IMesh
	{
		public IReadOnlyList<IMaterial>[] Materials => throw new NotImplementedException();

		public Mesh(RootDriver driver) : base(driver) { }

		public void SetMaterial(IMaterial material)
		{
			throw new NotImplementedException();
		}

		public void SetMaterial(int index, IMaterial material)
		{
			throw new NotImplementedException();
		}
	}
}
