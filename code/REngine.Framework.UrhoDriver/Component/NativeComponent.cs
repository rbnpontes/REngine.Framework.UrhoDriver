using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class NativeComponent : NativeObject, IComponent
	{
		public Guid Id => new Guid();

		public string Name { get; internal set; }

		public bool Enabled
		{
			get => Driver.ComponentDriver.IsEnabled(this);
			set => Driver.ComponentDriver.SetEnabled(this, value);
		}

		public IActor Owner { get => Driver.ComponentDriver.GetOwner(this); set => throw new NotSupportedException("Is not supported to Change Owner on Native Components"); }
		public IWorld World { get => Driver.ComponentDriver.GetWorld(this); set => throw new NotSupportedException("Is not supported to Change World on Native Components"); }

		public void OnAwake()
		{
			throw new NotSupportedException("OnAwake is not supported on Native Components");
		}

		public void OnDestroy()
		{
			throw new NotSupportedException("OnDestroy is not supported on Native Components");
		}

		public void OnRemove()
		{
			throw new NotSupportedException("OnRemove is not supported on Native Components");
		}

		public void OnStart()
		{
			throw new NotSupportedException("OnStart is not supported on Native Components");
		}

		public void Destroy()
		{
			Owner.RemoveComponent(GetType());
		}
	}
}
