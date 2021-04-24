﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace REngine.Framework.UrhoAppTest
{
	[TestClass]
	public class WorldTest : BaseTest
	{
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
	}
}