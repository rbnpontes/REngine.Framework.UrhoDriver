using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class UnknowComponent : NativeComponent, IUnknowComponent
	{
		public UnknowComponent(Handler handler, RootDriver driver) {
			Handle = handler;
			Driver = driver;
		}
	}
}
