using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	/// <summary>
	/// Specialized Urho Application
	/// </summary>
	public class UrhoApplication : Application
	{
		protected IEngine Engine { get; private set; }
		protected override void Init(IServiceCollection serviceCollection)
		{
			(Root.Driver as RootDriver).ComponentCollection.Collect();

			Engine = Root.CreateEngine();

			serviceCollection.AddSingleton<IEngine>(Engine);
			Engine.Init();
		}

		protected override void Start(IServiceProvider serviceProvider)
		{
		}

		protected override void Stop(IServiceProvider serviceProvider)
		{
			Engine.Stop();
		}

		protected override void Update(IServiceProvider serviceProvider)
		{
			Engine.NextFrame();
		}
	}
}
