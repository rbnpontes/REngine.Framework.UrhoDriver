using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class Engine : NativeObject, IEngine
	{
		private bool _stopped = false;
		public Engine(Handler handle, RootDriver driver) :base(handle, driver) { }

		public void Dispose()
		{
			Stop();
		}

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
			if (_stopped)
				return;
			Driver.EngineDriver.Stop(this);
			_stopped = true;
		}
	}
}
