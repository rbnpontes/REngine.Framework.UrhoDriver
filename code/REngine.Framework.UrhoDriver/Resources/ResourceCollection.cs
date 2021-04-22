using REngine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	sealed class ResourceCollection : IResourcesCollection
	{
		private IDictionary<Type, ResourceConstructor> _resourcesInfo = new Dictionary<Type, ResourceConstructor>();

		public ResourceConstructor[] ResourcesInfo
		{
			get => _resourcesInfo.Values.ToArray();
		}

		public RootDriver RootDriver { get; set; }
		public ResourceCollection(RootDriver driver) => RootDriver = driver;

		public ResourceCollection()
		{
			RegisterDefaults();
		}

		private void RegisterDefaults()
		{
			Add<IMesh, Mesh>();
		}

		private void ValidateType(Type type)
		{
			if (_resourcesInfo.ContainsKey(type))
				throw new ArgumentException("Resource Type has already added.");
		}

		public IResourcesCollection Add(Type type)
		{
			ValidateType(type);
			_resourcesInfo[type] = new ResourceConstructor(type);
			return this;
		}

		public IResourcesCollection Add<T>()
		{
			return Add(typeof(T));
		}

		public IResourcesCollection Add<Interface, Type>()
		{
			return Add(typeof(Type), typeof(Interface));
		}

		public IResourcesCollection Add(Type type, Type @interface)
		{
			ValidateType(@interface);
			_resourcesInfo[@interface] = new ResourceConstructor(type, @interface);
			return this;
		}

		public IResourcesCollection Add(Type @interface, Func<IResource> ctorFn)
		{
			ValidateType(@interface);
			_resourcesInfo[@interface] = new ResourceConstructor(@interface, ()=> ctorFn());
			return this;
		}

		public IResourcesCollection Add<Interface>(Func<Interface> ctorFn)
		{
			Type typeInterface = typeof(Interface);
			ValidateType(typeof(Interface));
			_resourcesInfo[typeInterface] = new ResourceConstructor(typeInterface, ()=> ctorFn());
			return this;
		}

		public IResourceManager Build()
		{
			return new ResourceManager(_resourcesInfo, RootDriver);
		}
	}
}
