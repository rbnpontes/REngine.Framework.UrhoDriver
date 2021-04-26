using Microsoft.VisualStudio.TestTools.UnitTesting;
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
	}
}
