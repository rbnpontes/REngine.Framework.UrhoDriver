using REngine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	internal abstract class BaseResource : IResource
	{
		public Guid Id { get; private set; } = new Guid();

		public string Name { get; internal set; }
		
		public bool IsNative { get; private set; }

		public BaseResource(bool isNative)
		{
			IsNative = isNative;
		}
	}
}
