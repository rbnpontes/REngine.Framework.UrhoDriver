using Microsoft.VisualStudio.TestTools.UnitTesting;
using REngine.Framework.UrhoAppTest.Components;
using REngine.Framework.UrhoDriver;
using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoAppTest
{
	[TestClass]
	public class ActorTest : BaseTest
	{
		[TestMethod]
		public void Test_Name()
		{
			const string actorName = "I'm a Actor";
			using (IWorld world = Root.CreateWorld())
			{
				IActor actor = world.CreateActor();
				actor.Name = actorName;

				Assert.AreEqual(actor.Name, actorName);
			}
		}
		
		[TestMethod]
		public void Test_Children()
		{
			using (IWorld world = Root.CreateWorld())
			{
				IActor parent = world.CreateActor();
				IActor child = world.CreateActor();

				parent.AddChild(child);
				Assert.AreEqual(child, parent.Children[0]);
				Assert.AreEqual(child.Parent, parent);
			}
		}

		[TestMethod]
		public void Test_Destroy()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor parent = world.CreateActor();
				IActor child = world.CreateActor();

				parent.AddChild(child);
				child.Destroy();

				Assert.AreEqual(parent.Children.Count, 0);
				parent.Destroy();
				Assert.AreEqual(world.Actors.Count, 0);

				parent = world.CreateActor();
				child = world.CreateActor();

				parent.AddChild(child);
				parent.Destroy();

				Assert.AreEqual(0, world.Actors.Count);
				Assert.IsTrue(Driver.CoreDriver.HasDestroyed(parent.Handle));
				Assert.IsTrue(Driver.CoreDriver.HasDestroyed(child.Handle));
			}
		}

		[TestMethod]
		public void Test_Detach()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor parent = world.CreateActor();
				IActor child = world.CreateActor();

				parent.AddChild(child);
				Assert.AreEqual(world.Actors.Count, 1);
				child.Detach(); // Detach will remove parent dependency and returns to World

				var actors = world.Actors;
				Assert.AreEqual(actors.Count, 2);
			}
		}

		[TestMethod]
		public void Test_World()
		{
			IActor parent;
			
			using(IWorld world = Root.CreateWorld())
			{
				parent = world.CreateActor();
				ReferenceHolder referenceHolder = HandleUtils.TryGetReferenceHolder(parent.Handle);
				
				Assert.IsNotNull(world);
				Assert.AreEqual(world, parent.World);
				Assert.IsNotNull(referenceHolder);
				Assert.IsTrue(referenceHolder.IsStrong);
				Assert.IsFalse(referenceHolder.IsWeak);
			}

			Assert.IsTrue(HandleUtils.IsDestroyed(parent.Handle));
		}

		[TestMethod]
		public void Test_Actor_After_Destroy()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor puppetActor = world.CreateActor();
				IActor actor = world.CreateActor();
				actor.Destroy();

				Assert.IsTrue(HandleUtils.IsDestroyed(actor.Handle));
				Assert.IsNull(HandleUtils.TryGetReferenceHolder(actor.Handle));

				// IActor methods can't throw any exception.
				actor.AddChild(puppetActor);
				actor.RemoveChild(puppetActor);
				actor.Destroy();
				actor.Detach();

				actor.Enabled = true;
				Assert.IsFalse(actor.Enabled);

				actor.CreateComponent<TestComponent>();
				Assert.IsNull(actor.Clone());
				Assert.IsNull(actor.GetComponent<TestComponent>());
				Assert.IsNotNull(actor.GetAllComponents());
				Assert.AreEqual(0, actor.GetAllComponents().Count);
				Assert.IsFalse(actor.HasComponent<TestComponent>());
				actor.RemoveComponent<TestComponent>();

				string testStr = "Actor";
				actor.Name = testStr;
				Assert.AreNotEqual(testStr, actor.Name);
				Assert.IsTrue(string.IsNullOrEmpty(actor.Name));
				Assert.AreEqual(-1, actor.Id);
				Assert.IsNull(actor.Parent);
				Assert.IsNull(actor.World);
				Assert.IsNotNull(actor.Children);
				Assert.Equals(0, actor.Children.Count);
			}


		}
	}
}
