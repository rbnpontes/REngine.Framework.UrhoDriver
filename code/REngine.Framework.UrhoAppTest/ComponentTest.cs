using Microsoft.VisualStudio.TestTools.UnitTesting;
using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoAppTest
{
	[TestClass]
	public class ComponentTest : BaseTest
	{
		[TestMethod]
		public void Test_Component_Native_Add()
		{
			using (IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();

				ILight light = actor.CreateComponent<ILight>();

				Assert.IsNotNull(light);
			}
		}

	}
}
