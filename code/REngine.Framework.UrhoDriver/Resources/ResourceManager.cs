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
			ValidateThread();
			throw new NotImplementedException();
		}

		public Task<T> AsyncGet<T>(string name)
		{
			throw new NotImplementedException();
		}

		public IResourceManager ClearCache()
		{
			ValidateThread();
			_cachedResources.Clear();

			return this;
		}

		public void Dispose()
		{
			ValidateThread();
			ClearCache();
		}

		public T Get<T>(string name)
		{
			return (T)Get(typeof(T), name);
		}

		public IResource Get(Type type, string name)
		{
			ValidateThread();
			ValidateType(type);
			IResource resource = GetFromCache(name);

			if(resource is null)
			{
				ResourceConstructor resourceCtor = _resourcesInfo[type];
				resource = resourceCtor.Instantiate();
			} else
			{
				return resource;
			}
			

			bool isNative = resource is NativeResource;

			if (isNative)
				LoadNativeResource(resource, name);
			else
				LoadManagedResource(resource, name);

			return resource;
		}

		private IResource GetFromCache(string res)
		{
			if (_cachedResources.ContainsKey(res))
				return _cachedResources[res];
			return null;
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

		/// <summary>
		/// Throws a Exception is in on another thread instead of main. 
		/// </summary>
		private void ValidateThread()
		{
			if (System.Threading.Thread.CurrentThread != _rootDriver.CurrentThread)
				throw new AccessViolationException("ResourceManager can only be accessed on Main Thread");
		}

		private void ValidateType(Type type)
		{
			if (!_resourcesInfo.ContainsKey(type))
				throw new ArgumentException("This resource type is not registered!");
		}
	}
}
