using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class WorldInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Scene_Create(IntPtr ctx);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Scene_Clear(IntPtr scene);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Scene_CreateChild(IntPtr scene);
	}
}
