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

		public Mesh() : base() { }

		public void SetMaterial(IMaterial material)
		{
			throw new NotImplementedException();
		}

		public void SetMaterial(int index, IMaterial material)
		{
			throw new NotImplementedException();
		}

		public override void LoadResource(Handler cache, string name)
		{
			Handler handler = RootDriver.ResourceManagerDriver.LoadResource(Drivers.ResourceType.Model, cache, name);
			Name = name;
			Handle = handler;
		}
	}
}
