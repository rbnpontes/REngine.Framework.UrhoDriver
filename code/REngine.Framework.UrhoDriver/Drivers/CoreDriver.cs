using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal sealed class CoreDriver : BaseDriver, ICoreDriver
	{
		public CoreDriver(RootDriver driver) : base(driver)
		{
		}

		public void Destroy(IHandle handle)
		{
			ValidateThread();
			CoreInternals.Object_Free(GetPointerFromHandle(handle));
		}

		public void Dump()
		{
			// Does nothing
		}

		public IHandle GetHandle(object obj)
		{
			return (obj as NativeObject).Handle;
		}

		public bool HasDestroyed(IHandle handle)
		{
			ValidateThread();
			return (handle as Handler).IsDestroyed;
		}
	}
}
