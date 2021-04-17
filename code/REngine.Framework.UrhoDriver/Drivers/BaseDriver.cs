using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class BaseDriver
	{
		public RootDriver RootDriver { get; private set; }

		public BaseDriver(RootDriver driver) => RootDriver = driver;

		public IntPtr GetPointerFromNativeObj(NativeObject obj)
		{
			IHandle handle = obj.Handle;
			return (IntPtr)handle.Obj;
		}
		public IntPtr GetPointerFromObj(object obj)
		{
			return GetPointerFromNativeObj(obj as NativeObject);
		}
	}
}
