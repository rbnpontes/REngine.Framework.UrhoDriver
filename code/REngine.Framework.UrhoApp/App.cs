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
		void TestEqualityActors(IWorld world)
		{
			IActor first = world.CreateActor();
			IActor second = world.Actors[0];

			if (first == second)
				Console.WriteLine("Actors is equals");
		}
		protected override void Start(IServiceProvider serviceProvider)
		{
			IWorld world = Root.CreateWorld();
			TestEqualityActors(world);

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			IMesh box = ResourceManager.Get<IMesh>("Models/Box.mdl");
		}
	}
}
