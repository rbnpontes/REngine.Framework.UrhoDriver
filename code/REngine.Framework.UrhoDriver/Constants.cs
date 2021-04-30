using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	public static class Constants
	{
		public const string LibPath = "Urho3D_d.dll";

		// Empty Lists for help incoming instantiation of empty list
		// Maybe this approach can help on memory reduce.
		public static readonly IReadOnlyList<IComponent> EmptyComponentList = new EmptyReadOnlyList<IComponent>();
		public static readonly IReadOnlyList<IActor> EmptyActorList = new EmptyReadOnlyList<IActor>();
	}
}
