using Microsoft.VisualStudio.TestTools.UnitTesting;
using REngine.Framework.UrhoDriver;
using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;

namespace REngine.Framework.UrhoAppTest
{
	[TestClass]
	public class WorldTest : BaseTest
	{
		[TestMethod]
		public void Test_Clear()
		{
			using(IWorld world = Root.CreateWorld())
			{
				for (int i = 0; i < 10; i++)
					world.CreateActor();

				world.Clear();
				Assert.AreEqual(world.Actors.Count, 0);
			}
		}
		[TestMethod]
		public void Test_CreateActor()
		{
			using (IWorld world = Root.CreateWorld())
			{
				Assert.IsNotNull(world.CreateActor());
			}
		}
		[TestMethod]
		public void Test_Children()
		{
			using(IWorld world = Root.CreateWorld())
			{
				IActor first = world.CreateActor();
				IActor second = world.CreateActor();
				var actors = world.Actors;

				Assert.AreEqual(actors.Count, 2);
				Assert.AreEqual(first, actors[0]);
				Assert.AreEqual(second, actors[1]);
			}
		}
		
		[TestMethod]
		public void Test_World_After_Destroy()
		{
			IWorld world = Root.CreateWorld();
			HandleUtils.ForceDestroy(world.Handle);

			// IWorld methods can't throw any exception after handle destroy
			Assert.IsNull(world.CreateActor());
			world.Clear();
			world.Dispose();

			Assert.IsNull(HandleUtils.TryGetReferenceHolder(world.Handle));
		}
	}
}
