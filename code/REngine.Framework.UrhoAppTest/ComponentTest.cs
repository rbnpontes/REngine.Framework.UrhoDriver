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
		public ComponentTest() : base()
		{
			CollectComponents();
		}
		// Before create any Component, is necessary to Collect components first
		private void CollectComponents()
		{
			(Driver as RootDriver).ComponentCollection.Collect();
		}

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

		[TestMethod]
		public void Test_Component_Managed_Add()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();
				TestComponent component = actor.CreateComponent<TestComponent>();

				Assert.IsNotNull(component);
			}
		}

		[TestMethod]
		public void Test_Component_Native_Get()
		{
			using(IWorld world = Root.CreateWorld()) {
				IActor actor = world.CreateActor();

				actor.CreateComponent<ILight>();
				Assert.IsNotNull(actor.GetComponent<ILight>());
			}
		}

		[TestMethod]
		public void Test_Component_Managed_Get()
		{
			using (IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();

				actor.CreateComponent<TestComponent>();
				Assert.IsNotNull(actor.GetComponent<TestComponent>());
			}
		}

		[TestMethod]
		public void Test_Component_Both_Get()
		{
			using (IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();

				actor.CreateComponent<ILight>();
				actor.CreateComponent<TestComponent>();

				Assert.IsNotNull(actor.GetComponent<ILight>());
				Assert.IsNotNull(actor.GetComponent<TestComponent>());
			}
		}

		[TestMethod]
		public void Test_Component_GetAllComponents()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();

				actor.CreateComponent<ILight>();
				actor.CreateComponent<TestComponent>();

				var components = actor.GetAllComponents();

				Assert.AreEqual(2, components.Count);
				Assert.IsNotNull(components[0]);
				Assert.IsNotNull(components[1]);
				Assert.AreEqual("Light", components[0].Name);
				Assert.AreEqual("TestComponent", components[1].Name);
			}
		}

		[TestMethod]
		public void Test_Component_Lifecycle()
		{
			IWorld world = Root.CreateWorld();
			IActor actor = world.CreateActor();

			bool destroy = false;

			TestComponent component = actor.CreateComponent<TestComponent>();

			EventHandler destroyFn = (sender, e) => destroy = true;

			component.OnDestroyEvent += destroyFn;

			actor.RemoveComponent<TestComponent>();

			component.OnDestroyEvent -= destroyFn;

			Assert.IsTrue(destroy);
		}
	}
}
