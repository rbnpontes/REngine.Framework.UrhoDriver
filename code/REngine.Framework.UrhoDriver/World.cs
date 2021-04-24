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
		public IReadOnlyList<IActor> Actors
		{
			get
			{
				return Driver.WorldDriver.GetActors(this);
			}
		}

		public World(Handler handle, RootDriver driver) : base(handle, driver) {
#if DEBUG
			handle.TypeName = nameof(World);
#endif
		}

		public IWorld Clear()
		{
			Driver.WorldDriver.Clear(this);
			return this;
		}

		public object Clone()
		{
			throw new NotImplementedException();
		}

		public IActor CreateActor()
		{
			return Driver.WorldDriver.CreateActor(this);
		}

		public void Dispose()
		{
			Clear();
		}
	}
}
