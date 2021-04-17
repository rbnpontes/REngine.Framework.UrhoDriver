using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	delegate void DestroyCallback(IntPtr pointer);
	internal sealed class Handler : IHandle
	{
		private IntPtr ptr = IntPtr.Zero;
		private DestroyCallback destroyCallback;
		private bool destroyed = false;

		public bool IsDestroyed { get => destroyed; }

		public string Name { get; set; }

		public object Obj
		{
			get => ptr;
			set
			{
				ptr = (IntPtr)value;
			}
		}
		public static Handler Zero { get; private set; } = new Handler(IntPtr.Zero);

		public Handler(IntPtr pointer)
		{
			ptr = pointer;

			if (pointer.Equals(IntPtr.Zero))
			{
				destroyed = true;
				Name = "undefined";
				return;
			}
			
			destroyCallback = (ptr) =>
			{
				destroyed = true;
				ptr = IntPtr.Zero;
			};


		}
		public void Reset()
		{
			ptr = IntPtr.Zero;
			destroyed = true;
		}

		public bool Equals(IHandle other)
		{
			return ptr.Equals(other.Obj);
		}

		public static implicit operator Handler(IntPtr ptr) => new Handler(ptr);
	}
}
