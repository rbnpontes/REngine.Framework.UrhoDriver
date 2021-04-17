using Microsoft.Extensions.DependencyInjection;
using REngine.Framework.UrhoDriver;
using REngine.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoApp
{
	class App : Application
	{
		private IEngine _engine;
		protected override void Init(IServiceCollection serviceCollection)
		{
			Root.Driver = new RootDriver();

			_engine = Root.CreateEngine();

			serviceCollection.AddSingleton<IEngine>(_engine);
			_engine.Init();
		}

		protected override void Start(IServiceProvider serviceProvider)
		{
		}

		protected override void Stop(IServiceProvider serviceProvider)
		{
			_engine.Stop();
		}

		protected override void Update(IServiceProvider serviceProvider)
		{
			_engine.NextFrame();
		}
	}
}
