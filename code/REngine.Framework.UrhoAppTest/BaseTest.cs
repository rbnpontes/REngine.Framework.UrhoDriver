using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver;
using System;

namespace REngine.Framework.UrhoAppTest
{
	public class BaseTest
	{
		public Root Root { get; private set; }
		public IEngine Engine { get; private set; }
		public IRootDriver Driver { get; private set; }

		public BaseTest()
		{
			Initialize();
		}

		~BaseTest()
		{
			Engine.Dispose();
			Root.Dispose();
		}
		private void Initialize()
		{
			Root = new Root();
			Root.Driver = Driver = new RootDriver();
			Engine = Root.CreateEngine();
			Engine.Init();
		}

		protected void ForceGC()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.WaitForFullGCComplete();
			GC.Collect();
		}
	}
}
