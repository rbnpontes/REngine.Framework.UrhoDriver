using Microsoft.Extensions.DependencyInjection;
using REngine.Framework.Resources;
using REngine.Framework.UrhoDriver;
using REngine.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoApp
{
	class App : UrhoApplication
	{
		protected override void Start(IServiceProvider serviceProvider)
		{
			IMesh box = ResourceManager.Get<IMesh>("Models/Box.mdl");
		}
	}
}
