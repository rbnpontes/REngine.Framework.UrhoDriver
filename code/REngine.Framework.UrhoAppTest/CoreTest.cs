using Microsoft.VisualStudio.TestTools.UnitTesting;
using REngine.Framework.UrhoDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoAppTest
{
	[TestClass]
	public class CoreTest : BaseTest
	{
		private void MemoryLeakTest()
		{
			IWorld world = Root.CreateWorld();
			for (int i = 0; i < 10; i++)
				world.CreateActor();
		}
		[TestMethod]
		public void Test_MemoryLeak()
		{
			using (MemoryTracer tracer = MemoryTracer.Begin())
			{
				MemoryLeakTest();

				ForceGC();

				tracer.End();

				(uint dealloc, uint unalloc) = tracer.GetStatus();

				Assert.AreEqual(unalloc, 0u);
			}
		}
	}
}
