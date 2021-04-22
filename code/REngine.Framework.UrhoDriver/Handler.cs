using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	delegate void DriverObjectCallback(IntPtr pointer);
	delegate void DriverObjectAlignCallback(IntPtr pointer, int sessionId);
	internal sealed class Handler : IHandle
	{
		private IntPtr ptr = IntPtr.Zero;


		private DriverObjectCallback _addRefCallback;
		private DriverObjectCallback _removeRefCallback;
		private DriverObjectCallback _destroyCallback;
		private DriverObjectAlignCallback _alignCallback;

		private bool _isStrong = false;
		private int _sessionId = -1;

		private bool destroyed = false;

		public bool IsDestroyed { get => destroyed; }

		public int Refs
		{
			get => CoreInternals.Object_Refs(ptr);
		}

		public object Obj
		{
			get => ptr;
		}
		public static Handler Zero { get; private set; } = new Handler(IntPtr.Zero);

		public Handler(IntPtr pointer)
		{
			ptr = pointer;

			if (pointer.Equals(IntPtr.Zero))
			{
				destroyed = true;
				return;
			}

			_isStrong = Refs != 0;
			
			RegisterListeners();
		}

		~Handler()
		{
			// Return is was destroyed by unmanaged.
			if (destroyed)
				return;

			if (_isStrong)
			{
				ClearListeners(true)
;				return;
			}

			CoreInternals.Object_Free(ptr);
		}

		private void RegisterListeners()
		{
			_sessionId = CoreInternals.Object_CreateCallbackSession(ptr);

			_destroyCallback = (ptr) =>
			{
				destroyed = true;
				ptr = IntPtr.Zero;
				ClearListeners(false);
			};

			_addRefCallback = (ptr) =>
			{
				_isStrong = true;
			};

			_removeRefCallback = (ptr) =>
			{
				// Does nothing
			};

			_alignCallback = (ptr, id) =>
			{
				_sessionId = id;
			};

			CoreInternals.Object_SetupCallbackSession(ptr, _sessionId, 
				_addRefCallback, 
				_removeRefCallback, 
				_destroyCallback, 
				_alignCallback);
		}

		private void ClearListeners(bool clearNative)
		{
			_destroyCallback = _removeRefCallback = _addRefCallback = null;
			_alignCallback = null;

			if(clearNative)
				CoreInternals.Object_DropSession(ptr, _sessionId);
		}

		public bool Equals(IHandle other)
		{
			return ptr.Equals(other.Obj);
		}

		public static implicit operator Handler(IntPtr ptr) => new Handler(ptr);
	}
}
