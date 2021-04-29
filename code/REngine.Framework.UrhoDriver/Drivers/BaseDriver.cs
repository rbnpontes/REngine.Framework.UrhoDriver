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

		public T TryBindReferenceHolder<T>(IntPtr ptr, Func<Handler, T> ctor)
		{
			T result = default(T);
			if (ptr.Equals(IntPtr.Zero))
				return result;
			IntPtr gcHandlePtr = CoreInternals.Object_GetManagedRefPtr(ptr);
			if(gcHandlePtr.Equals(IntPtr.Zero))
			{
				Handler targetHandler = ptr;
				result = ctor(targetHandler);

				ReferenceHolder referenceHolder = new ReferenceHolder(result);
				GCHandle gCHandle = GCHandle.Alloc(referenceHolder);

				targetHandler.OnAdd += Handler_HandleAdd;
				targetHandler.OnRelease += Handler_HandleRelease;
				targetHandler.OnDestroy += Handler_HandleDestroy;

				CoreInternals.Object_SetManagedRefPtr(ptr, GCHandle.ToIntPtr(gCHandle));
			} else
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(gcHandlePtr);
				ReferenceHolder referenceHolder = gCHandle.Target as ReferenceHolder;
				result = (T)referenceHolder.Target;
			}

			return result;
		}

		private void Handler_HandleAdd(object sender, EventArgs e)
		{
			Handler handler = sender as Handler;
			GCHandle? gcHandle = GetGCHandleFromHandle(handler);
			if (gcHandle is null)
				return;
			ReferenceHolder refHolder = gcHandle.Value.Target as ReferenceHolder;
			refHolder.MakeStrong();
		}

		private void Handler_HandleRelease(object sender, EventArgs e)
		{
			Handler handler = sender as Handler;
			if (!handler.IsStrong)
				return;
			GCHandle? gcHandle = GetGCHandleFromHandle(handler);
			if (gcHandle is null)
				return;
			ReferenceHolder refHolder = gcHandle.Value.Target as ReferenceHolder;
			refHolder.MakeWeak();
		}

		private void Handler_HandleDestroy(object sender, EventArgs e)
		{
			Handler handler = sender as Handler;
			GCHandle? gcHandle = GetGCHandleFromHandle(handler);
			if (gcHandle is null)
				return;
			ReferenceHolder refHolder = gcHandle.Value.Target as ReferenceHolder;
			refHolder.MakeWeak();
			gcHandle.Value.Free();
		}

		public IntPtr GetPointerFromNativeObj(NativeObject obj)
		{
			if (obj is null)
				return IntPtr.Zero;
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
