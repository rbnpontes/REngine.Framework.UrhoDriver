using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;

namespace REngine.Framework.UrhoDriver
{
	delegate void DriverObjectCallback(IntPtr pointer);
	internal sealed class Handler : IHandle, IEquatable<IHandle>
	{
		private IntPtr ptr = IntPtr.Zero;


		private DriverObjectCallback _addRefCallback;
		private DriverObjectCallback _removeRefCallback;
		private DriverObjectCallback _destroyCallback;

		public event EventHandler OnAdd;
		public event EventHandler OnRelease;
		public event EventHandler OnDestroy;

		private bool _isStrong = false;

		private bool destroyed = false;

		public bool IsStrong { get => _isStrong; }

		public bool IsDestroyed { get => destroyed; }

		public int Refs
		{
			get => CoreInternals.Object_Refs(ptr);
		}

		public object Obj
		{
			get => ptr;
		}

#if DEBUG // Debug Purposes Only
		public string TypeName { get; set; } = "";
#endif
		public static Handler Zero { get; private set; } = new Handler(IntPtr.Zero);

		public Handler(IntPtr pointer)
		{
			ptr = pointer;

			if (pointer.Equals(IntPtr.Zero))
			{
				destroyed = true;
				return;
			}

			_isStrong = Refs > 0;
			
			RegisterListeners();
		}

		~Handler()
		{
			// Return is was destroyed by unmanaged.
			if (destroyed)
				return;

			if (_isStrong)
				return;

			CoreInternals.Object_Free(ptr);
		}

		private void RegisterListeners()
		{
			_destroyCallback = (ptr) =>
			{
				destroyed = true;
				OnDestroy?.Invoke(this, new EventArgs());
				ptr = IntPtr.Zero;
			};

			_addRefCallback = (ptr) =>
			{
				_isStrong = true;
				OnAdd?.Invoke(this, new EventArgs());
			};

			_removeRefCallback = (ptr) =>
			{
				_isStrong = Refs > 0;
				OnRelease?.Invoke(this, new EventArgs());
			};

			CoreInternals.Object_SetupCallbackSession(ptr, 
				_addRefCallback, 
				_removeRefCallback, 
				_destroyCallback);
		}

		public bool Equals(IHandle other)
		{
			return other.GetHashCode() == GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == GetHashCode();
		}

		public override int GetHashCode()
		{
			return (int)ptr.ToInt64();
		}

		public static implicit operator Handler(IntPtr ptr) => new Handler(ptr);
	}
}
