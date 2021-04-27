using Microsoft.VisualStudio.TestTools.UnitTesting;
using REngine.Framework.Components;
using REngine.Framework.UrhoAppTest.Components;
using REngine.Framework.UrhoDriver;
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
		// Before create any Component, is necessary to Collect components first
		private void CollectComponents()
		{
			(Driver as RootDriver).ComponentCollection.Collect();
		}

		[TestMethod]
		public void Test_Component_Native_Add()
		{
			CollectComponents();
			using (IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();

				ILight light = actor.CreateComponent<ILight>();

				Assert.IsNotNull(light);
			}
		}

		[TestMethod]
		public void Test_Component_Managed_Add()
		{
			CollectComponents();
			using(IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();
				TestComponent component = actor.CreateComponent<TestComponent>();

				Assert.IsNotNull(component);
			}
		}
	}
}
