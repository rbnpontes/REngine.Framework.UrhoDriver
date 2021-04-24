using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class ListInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr ManagedList_Get(IntPtr list, uint idx);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint ManagedList_GetLength(IntPtr list);
	}
}
