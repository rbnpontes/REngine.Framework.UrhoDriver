using REngine.Framework.Drivers;
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
				Validate();
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
			if (IsDestroyed)
				throw new AccessViolationException("This object has been already destroyed!!!");
		}
	}
}
