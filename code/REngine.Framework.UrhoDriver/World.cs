using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class World : NativeObject, IWorld
	{
		public IReadOnlyList<IActor> Actors => throw new NotImplementedException();

		public World(Handler handle, RootDriver driver) : base(handle, driver) { }

		public IWorld AddChild(IActor actor)
		{
			throw new NotImplementedException();
		}

		public IWorld Clear()
		{
			throw new NotImplementedException();
		}

		public object Clone()
		{
			throw new NotImplementedException();
		}

		public IActor CreateActor()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
