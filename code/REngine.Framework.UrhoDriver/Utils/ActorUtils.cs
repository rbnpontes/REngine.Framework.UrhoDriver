using REngine.Framework.UrhoDriver.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Utils
{
	public static class ActorUtils
	{
		public static ComponentScope GetComponentScope(IActor actor)
		{
			return (actor as Actor).ComponentScope;
		}
	}
}
