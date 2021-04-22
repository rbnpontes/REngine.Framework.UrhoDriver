using REngine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Resources
{
	internal class ResourceManager : IResourceManager
	{
		private IDictionary<string, IResource> _cachedResources = new Dictionary<string, IResource>();
		public IReadOnlyList<IResource> Resources => throw new NotImplementedException();

		private IDictionary<Type, ResourceConstructor> _resourcesInfo;
		private RootDriver _rootDriver;

		public ResourceManager(IDictionary<Type, ResourceConstructor> resourcesInfo, RootDriver driver)
		{
			_resourcesInfo = resourcesInfo;
			_rootDriver = driver;
		}

		public Task<object> AsyncGet(Type type)
		{
			throw new NotImplementedException();
		}

		public Task<T> AsyncGet<T>(string name)
		{
			throw new NotImplementedException();
		}

		public IResourceManager ClearCache()
		{
			_cachedResources.Clear();

			return this;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public T Get<T>(string name)
		{
			throw new NotImplementedException();
		}

		public object Get(Type type, string name)
		{
			throw new NotImplementedException();
		}
	}
}
