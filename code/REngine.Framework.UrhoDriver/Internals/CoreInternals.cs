using System;
using System.Runtime.InteropServices;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class CoreInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_Free(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern int Object_Refs(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern int Object_CreateCallbackSession(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_SetupCallbackSession(IntPtr ptr, int idx, 
			DriverObjectCallback addCallback,
			DriverObjectCallback releaseCallback, 
			DriverObjectCallback destroyCallback,
			DriverObjectAlignCallback alignCallback);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_DropSession(IntPtr ptr, int idx);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Context_New();
	}
}
