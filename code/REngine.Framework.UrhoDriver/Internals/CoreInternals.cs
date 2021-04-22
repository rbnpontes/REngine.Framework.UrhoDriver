using System;
using System.Runtime.InteropServices;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class CoreInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_Free(IntPtr ptr);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Object_SetDestroyCallback(IntPtr ptr, DestroyCallback callback);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Context_New();
	}
}
