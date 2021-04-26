using System;
using System.Runtime.InteropServices;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class CoreInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Object_GetManagedRefPtr(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_SetManagedRefPtr(IntPtr ptr, IntPtr pinnedHandle);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_Free(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern int Object_Refs(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_SetupCallbackSession(IntPtr ptr, 
			DriverObjectCallback addCallback,
			DriverObjectCallback releaseCallback, 
			DriverObjectCallback destroyCallback);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Context_New();

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong Object_GetObjectCount();
	}
}
