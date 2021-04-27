using REngine.Framework.Components;
using REngine.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Component
{
	public sealed class ComponentCollection
	{
		private object _syncNativeObj = new object();
		private object _syncManagedObj = new object();
		private IDictionary<uint, ComponentInfo> _nativeComponents = new Dictionary<uint, ComponentInfo>();
		private IDictionary<Type, ComponentInfo> _components = new Dictionary<Type, ComponentInfo>();

		public void Collect()
		{
			_nativeComponents.Clear();
			_components.Clear();

			CollectTypes();
		}

		private void CollectTypes()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Parallel.ForEach(assemblies, assembly =>
			{
				assembly.GetTypes().ToList().ForEach(type =>
				{
					CollectType(type);
				});
			});
		}

		private void CollectType(Type type)
		{
			NativeComponentAttribute nativeComponentAttribute = type.GetCustomAttribute<NativeComponentAttribute>();
			ComponentAttribute componentAttr = type.GetCustomAttribute<ComponentAttribute>();

			if (componentAttr is null && nativeComponentAttribute is null)
				return;

			BaseComponentAttribute baseAttr = nativeComponentAttribute;
			if (baseAttr is null)
				baseAttr = componentAttr;

			ComponentInfo componentInfo = new ComponentInfo();
			componentInfo.IsNative = componentAttr is null;
			componentInfo.Type = baseAttr.InterfaceType ?? type;
			componentInfo.ImplType = type;
			componentInfo.Ctor = new TypeExtractor(type).CollectOnlyCtor();
			componentInfo.HashCode = nativeComponentAttribute?.HashCode ?? 0u;
			
			KeyValuePair<Type, ComponentInfo> componentPair = new KeyValuePair<Type, ComponentInfo>(baseAttr.InterfaceType ?? type, componentInfo);
			
			if(componentInfo.IsNative)
			{
				KeyValuePair<uint, ComponentInfo> pair = new KeyValuePair<uint, ComponentInfo>(nativeComponentAttribute.HashCode, componentInfo);
				lock (_syncNativeObj)
				{
					_nativeComponents.Add(pair);
				}
			}

			lock (_syncManagedObj)
				_components.Add(componentPair);
		}

		public ComponentScope BuildComponentScope(IActor target)
		{
			return new ComponentScope((Actor)target, this);
		}

		public bool TryGetComponentInfo(Type type, out ComponentInfo componentInfo)
		{
			return _components.TryGetValue(type, out componentInfo);
		}

		public bool TryGetNativeComponentInfo(uint hashCode, out ComponentInfo componentInfo)
		{
			return _nativeComponents.TryGetValue(hashCode, out componentInfo);
		}

		public IReadOnlyList<ComponentInfo> GetComponentInfos()
		{
			return _nativeComponents.Values.Concat(_components.Values).ToList().AsReadOnly();
		}
	}
}
