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
		// Used for resolve Native Resources
		private Handler _resourceCacheHandle;

		public ResourceManager(IDictionary<Type, ResourceConstructor> resourcesInfo, RootDriver driver)
		{
			_resourcesInfo = resourcesInfo;
			_rootDriver = driver;
			_resourceCacheHandle = driver.ResourceManagerDriver.GetResourceCacheHandle();
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
			ClearCache();
		}

		public T Get<T>(string name)
		{
			return (T)Get(typeof(T), name);
		}

		public IResource Get(Type type, string name)
		{
			ValidateType(type);
			ResourceConstructor resourceCtor = _resourcesInfo[type];
			IResource resource = resourceCtor.Instantiate();

			bool isNative = resource is NativeResource;

			if (isNative)
				LoadNativeResource(resource, name);
			else
				LoadManagedResource(resource, name);

			return resource;
		}

		private void LoadNativeResource(IResource resource, string path)
		{
			NativeResource nativeResource = resource as NativeResource;
			nativeResource.RootDriver = _rootDriver;
			nativeResource.LoadResource(_resourceCacheHandle, path);
		}

		private void LoadManagedResource(IResource resource, string path)
		{
			resource.BeginRead(path);
			resource.EndRead(path);
		}

		private void ValidateType(Type type)
		{
			if (!_resourcesInfo.ContainsKey(type))
				throw new ArgumentException("This resource type is not registered!");
		}
	}
}
