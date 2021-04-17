using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class EngineDriver : BaseDriver, IEngineDriver
	{
		public EngineDriver(RootDriver driver) : base(driver)
		{
		}

		public IEngine Create(Root root)
		{
			Handler handler = EngineInternals.DriverApplication_New();
			return new Engine(handler, RootDriver);
		}

		public void Initialize(IEngine engine)
		{
			EngineInternals.DriverApplication_Initialize(GetPointerFromObj(engine));
		}

		public void RunNextFrame(IEngine engine)
		{
			EngineInternals.DriverApplication_NextFrame(GetPointerFromObj(engine));
		}

		public void Stop(IEngine engine)
		{
			EngineInternals.DriverApplication_Stop(GetPointerFromObj(engine));
		}
	}
}
