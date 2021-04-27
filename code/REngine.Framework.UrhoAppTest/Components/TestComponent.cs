using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoAppTest.Components
{
	[Component(typeof(TestComponent))]
	public class TestComponent : Behaviour<TestComponent>
	{
	}
}
