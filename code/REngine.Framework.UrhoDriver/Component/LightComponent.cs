using REngine.Framework.Components;
using REngine.Framework.Mathf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	[NativeComponent("Light", typeof(ILight))]
	internal class LightComponent : NativeComponent, ILight
	{
		public LightMode Mode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float Intensity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float Distance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float Angle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float Power { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public bool CastShadow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public LightComponent() { }
	}
}
