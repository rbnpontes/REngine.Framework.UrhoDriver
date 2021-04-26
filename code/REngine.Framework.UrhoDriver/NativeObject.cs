using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Drivers;
using System;

namespace REngine.Framework.UrhoDriver
{
	internal class NativeObject
	{
		private Handler _handler;
		
		public RootDriver Driver { get; internal set; }
		
		public IHandle Handle
		{
			get
			{
				Validate();
				return _handler;
			}
			internal set
			{
				_handler = value as Handler;
			}
		}

		public bool IsDestroyed
		{
			get
			{
				return _handler.IsDestroyed;
			}
		}
		public NativeObject()
		{
			Handle = Handler.Zero;
		}
		
		public NativeObject(Handler handler, RootDriver driver)
		{
			_handler = handler;
			Driver = driver;
		}

		private void Validate()
		{
			if (System.Threading.Thread.CurrentThread != Driver.CurrentThread)
				throw new AccessViolationException("This object is only accessible on Main Thread");
		}

		public override bool Equals(object obj)
		{
			return Handle.Equals(obj);
		}
		public override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
	}
}
