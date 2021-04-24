using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class MemoryTracerInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void MemoryTracer_Begin();
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void MemoryTracer_End();
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void MemoryTracer_Clear();
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern string MemoryTracer_GetReport();
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void MemoryTracer_GetObjectStatus([Out]out uint unallocated, [Out] out uint alive);
	}
}
