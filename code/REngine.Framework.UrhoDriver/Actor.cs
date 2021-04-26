using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class Actor : NativeObject, IActor
	{
		public int Id { get => Driver.ActorDriver.GetId(this); }
		public string Name { 
			get => Driver.ActorDriver.GetName(this); 
			set => Driver.ActorDriver.SetName(this, value); 
		}
		public IWorld World { 
			get => Driver.ActorDriver.GetWorld(this); 
			set => throw new NotImplementedException(); 
		}
		public IActor Parent {
			get => Driver.ActorDriver.GetParent(this);
			set => Driver.ActorDriver.SetParent(this, value); 
		}

		public IReadOnlyList<IActor> Children
		{
			get
			{
				return Driver.ActorDriver.GetChildren(this);
			}
		}

		public Actor(Handler handler, RootDriver driver) : base(handler, driver) { }

		public IActor AddChild(IActor actor)
		{
			Driver.ActorDriver.AddChild(this, actor);
			return this;
		}

		public object Clone()
		{
			return Driver.ActorDriver.Clone(this);
		}

		public void Destroy()
		{
			Driver.ActorDriver.Destroy(this);
		}

		public IActor RemoveChild(IActor actor)
		{
			Driver.ActorDriver.RemoveChild(this, actor);
			return this;
		}

		public IActor Detach()
		{
			Driver.ActorDriver.Detach(this);
			return this;
		}
	}
}
