using System;
using System.Runtime.InteropServices;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class EngineInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr DriverApplication_New();

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool DriverApplication_Initialize(IntPtr app);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void DriverApplication_NextFrame(IntPtr app);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void DriverApplication_Stop(IntPtr app);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void DriverApplication_Destroy(IntPtr app);

	}
}
