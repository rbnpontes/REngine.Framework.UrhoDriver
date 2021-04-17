using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class Engine : NativeObject, IEngine
	{
		public Engine(Handler handle, RootDriver driver) :base(handle, driver) { }

		public void Init()
		{
			Driver.EngineDriver.Initialize(this);
		}

		public void NextFrame()
		{
			Driver.EngineDriver.RunNextFrame(this);
		}

		public void Stop()
		{
			Driver.EngineDriver.Stop(this);
		}
	}
}
