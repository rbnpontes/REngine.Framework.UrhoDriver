using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class ComponentInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		public extern static bool Component_IsEnabled(IntPtr component);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public extern static void Component_SetEnabled(IntPtr component, [MarshalAs(UnmanagedType.I1)] bool value);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr Component_GetScene(IntPtr component);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr Component_GetNode(IntPtr component);
	}
}
