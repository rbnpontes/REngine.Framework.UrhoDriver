using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class ActorDriver : BaseDriver, IActorDriver
	{
		public ActorDriver(RootDriver driver) : base(driver)
		{
		}

		public void AddChild(IActor src, IActor handle)
		{
			throw new NotImplementedException();
		}

		public IActor Clone(IActor actor)
		{
			throw new NotImplementedException();
		}

		public void Destroy(IActor handle)
		{
			throw new NotImplementedException();
		}

		public INativeList GetChildren(IActor handle)
		{
			throw new NotImplementedException();
		}

		public string GetName(IActor handle)
		{
			throw new NotImplementedException();
		}

		public void RemoveChild(IActor src, IActor handle)
		{
			throw new NotImplementedException();
		}

		public void SetName(IActor handle, string value)
		{
			throw new NotImplementedException();
		}

		public IActor Wrap(IHandle handle)
		{
			throw new NotImplementedException();
		}
	}
}
