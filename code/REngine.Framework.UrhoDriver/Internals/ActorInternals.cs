using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Internals
{
	internal static class ActorInternals
	{
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint Node_GetID(IntPtr ptr);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern string Node_GetName(IntPtr ptr);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_SetName(IntPtr ptr, string name);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_AddChild(IntPtr src, IntPtr target);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_RemoveChild(IntPtr src, IntPtr target);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Node_GetChildren(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_Destroy(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Node_Clone(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_Detach(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Node_GetScene(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Node_GetParent(IntPtr src);
		[DllImport(Constants.LibPath, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Node_SetParent(IntPtr src, IntPtr actor);
	}
}
