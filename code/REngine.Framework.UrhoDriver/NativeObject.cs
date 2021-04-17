using REngine.Framework.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class NativeObject
	{
		private Handler _handler;
		
		public RootDriver Driver { get; internal set; }
		
		public IHandle Handle
		{
			get => _handler;
			internal set
			{
				_handler = value as Handler;
			}
		}

		public bool IsDestroyed { get => _handler.IsDestroyed; }

		public NativeObject()
		{
			Handle = Handler.Zero;
		}
		
		public NativeObject(Handler handler, RootDriver driver)
		{
			_handler = handler;
			Driver = driver;
		}
	}
}
