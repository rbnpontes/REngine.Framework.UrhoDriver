using REngine.Framework.Components;
using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Component;
using REngine.Framework.UrhoDriver.Internals;
using REngine.Framework.UrhoDriver.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal class ComponentDriver : BaseDriver, IComponentDriver
	{
		public ComponentDriver(RootDriver driver) : base(driver) { }

		public bool IsEnabled(IComponent component)
		{
			throw new NotImplementedException();
		}

		public bool SetEnabled(IComponent component, bool value)
		{
			throw new NotImplementedException();
		}

		public uint GetTypeHashCode(Type type)
		{
			NativeComponentAttribute attribute = type.GetCustomAttribute<NativeComponentAttribute>();
			return attribute?.HashCode ?? HashUtils.SDBM(type.Name); // if type is not a component, try to create a hashcode by a name type
		}

		public IComponent Wrap(Type type, IHandle handle)
		{
			uint hashType = GetTypeHashCode(type);

			return Wrap(hashType, handle);
		}

		public IComponent Wrap(uint hashType, IHandle handle)
		{
			ComponentInfo cp;

			if (RootDriver.ComponentCollection.TryGetNativeComponentInfo(hashType, out cp))
				return new UnknowComponent(handle as Handler, RootDriver);

			NativeComponent component = cp.Ctor.Instantiate<NativeComponent>();
			component.Handle = handle;
			component.Driver = RootDriver;
			component.ResolveReferences();

			return component;
		}

		public IComponent ListGetterCallback(IntPtr handle)
		{
			IComponent component = null;
			uint hashCode = CoreInternals.Object_GetHashCode(handle);
			IntPtr pinnedPtr = CoreInternals.Object_GetManagedRefPtr(handle);

			if (pinnedPtr.Equals(IntPtr.Zero))
			{
				Handler handler = pinnedPtr;
				component = Wrap(hashCode, handler);
				SetObjectOnHandle(handler, component);
				handler.OnDestroy += HandlePtrDestroy;
			}
			else
			{
				GCHandle gcHandle = GCHandle.FromIntPtr(pinnedPtr);
				component = (IComponent)gcHandle.Target;
			}
			return component
		} 
	}
}
