using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
	}
}
