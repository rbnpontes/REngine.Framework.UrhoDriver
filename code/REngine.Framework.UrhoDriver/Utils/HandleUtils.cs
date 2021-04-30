using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Utils
{
	public static class HandleUtils
	{
		public static IntPtr GetPtrFromHandle(IHandle handle)
		{
			return (IntPtr)handle.Obj;
		}

		public static bool TryGetGCHandle(IHandle handle, out GCHandle? gcHandle)
		{
			IntPtr ptr = GetPtrFromHandle(handle);
			if (ptr.Equals(IntPtr.Zero))
			{
				gcHandle = null;
				return false;
			}
			IntPtr pinnedPtr = CoreInternals.Object_GetManagedRefPtr(ptr);
			if(pinnedPtr.Equals(IntPtr.Zero))
			{
				gcHandle = null;
				return false;
			} else
			{
				gcHandle = GCHandle.FromIntPtr(pinnedPtr);
				return true;
			}
		}

		public static ReferenceHolder TryGetReferenceHolder(IHandle handle)
		{
			GCHandle? gCHandle;
			TryGetGCHandle(handle, out gCHandle);

			if (gCHandle is null)
				return null;
			return gCHandle.Value.Target as ReferenceHolder;
		}

		public static bool IsDestroyed(IHandle handle)
		{
			return (handle as Handler).IsDestroyed;
		}

		public static void ForceDestroy(IHandle handle)
		{
			Handler handler = handle as Handler;
			handler.Destroy();
		}
	}
}
