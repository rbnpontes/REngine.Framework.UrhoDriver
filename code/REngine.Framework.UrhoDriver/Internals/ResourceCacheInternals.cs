using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class ResourceCacheInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ResourceCache_Get(IntPtr ctx);

		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ResourceCache_GetModel(IntPtr resourceCache, string name);
	}
}
