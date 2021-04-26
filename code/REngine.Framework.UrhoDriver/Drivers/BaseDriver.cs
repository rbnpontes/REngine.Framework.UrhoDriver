using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

		public IntPtr GetPointerFromHandle(IHandle handle)
		{
			return (IntPtr)(handle as Handler).Obj;
		}

		public GCHandle? GetGCHandleFromHandle(IHandle handle)
		{
			IntPtr ptr = GetPointerFromHandle(handle);
			IntPtr pinnedPtr = CoreInternals.Object_GetManagedRefPtr(ptr);

			if (pinnedPtr.Equals(IntPtr.Zero))
				return null;
			return GCHandle.FromIntPtr(pinnedPtr);
		}

		public GCHandle SetObjectOnHandle<T>(IHandle handle, T obj)
		{
			IntPtr ptr = GetPointerFromHandle(handle);
			GCHandle gcHandle = GCHandle.Alloc(obj);

			CoreInternals.Object_SetManagedRefPtr(ptr, GCHandle.ToIntPtr(gcHandle));
			return gcHandle;
		}
		
		public IntPtr GetManagedPtrFromHandle(IHandle handle)
		{
			return CoreInternals.Object_GetManagedRefPtr((IntPtr)handle.Obj);
		}

		protected EventHandler GetPtrAddDelegate(object target)
		{
			return (sender, e) =>
			{
				SetObjectOnHandle((IHandle)sender, target);
			};
		}

		protected EventHandler GetPtrReleaseDelegate(object target)
		{
			return (sender, e) =>
			{
				Handler handler = (Handler)sender;
				var gcHandle = GetGCHandleFromHandle(handler);
				if (!handler.IsStrong && gcHandle != null)
					gcHandle.Value.Free();

			};
		}
		
		protected void HandlePtrDestroy(object sender, EventArgs e)
		{
			Handler handler = (Handler)sender;
			IntPtr pinnedPtr = GetManagedPtrFromHandle(handler);
			
			// Skip GCHandle Destruction if object doesn't contains a Handle
			if (pinnedPtr.Equals(IntPtr.Zero))
				return;

			GCHandle gCHandle = GCHandle.FromIntPtr(pinnedPtr);
			gCHandle.Free();
		}

		protected void ValidateThread()
		{
			Thread currThread = Thread.CurrentThread;
			if (currThread != RootDriver.CurrentThread)
				throw new AccessViolationException("Main Resources can only be accessed by Main Thread!");
		}
	}
}
