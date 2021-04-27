using REngine.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	internal class NativeComponent : NativeObject, IComponent
	{
		public Guid Id => new Guid();

		public string Name { get; private set; }

		public IActor Owner { get => null; set => throw new NotSupportedException("Native Components can`t change Owner"); }
		public IWorld World { get => null; set => throw new NotSupportedException("Native Components can`t change World"); }

		public NativeComponent(string name)
		{
			Name = name;
		}
		/// <summary>
		/// Resolve Owner and World properties
		/// </summary>
		public void ResolveReferences()
		{

		}

		public void OnAwake()
		{
			throw new NotSupportedException("OnAwake is not supported on Native Components");
		}

		public void OnDestroy()
		{
			throw new NotSupportedException("OnDestroy is not supported on Native Components");
		}

		public void OnRemove()
		{
			throw new NotSupportedException("OnRemove is not supported on Native Components");
		}

		public void OnStart()
		{
			throw new NotSupportedException("OnStart is not supported on Native Components");
		}
	}
}
