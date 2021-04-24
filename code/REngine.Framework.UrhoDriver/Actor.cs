using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class Actor : NativeObject, IActor
	{
		public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IWorld World { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IActor Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public IReadOnlyList<IActor> Children => throw new NotImplementedException();

		public Actor(Handler handler, RootDriver driver) : base(handler, driver) { }

		public IActor AddChild(IActor actor)
		{
			throw new NotImplementedException();
		}

		public object Clone()
		{
			throw new NotImplementedException();
		}

		public void Destroy()
		{
			throw new NotImplementedException();
		}

		public IActor RemoveChild(IActor actor)
		{
			throw new NotImplementedException();
		}
	}
}
