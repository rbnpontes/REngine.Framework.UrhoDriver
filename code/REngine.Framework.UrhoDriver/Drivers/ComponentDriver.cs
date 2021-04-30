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
			return ComponentInternals.Component_IsEnabled(GetPointerFromObj(component));
		}

		public void SetEnabled(IComponent component, bool value)
		{
			ComponentInternals.Component_SetEnabled(GetPointerFromObj(component), value);
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
			NativeComponent component;
			if (!RootDriver.ComponentCollection.TryGetNativeComponentInfo(hashType, out cp))
				component = new UnknowComponent();
			else
				component = cp.Ctor.Instantiate<NativeComponent>();

			component.Handle = handle;
			component.Driver = RootDriver; 

			return component;
		}

		public IComponent ListGetterCallback(IntPtr handle)
		{
			if (handle.Equals(IntPtr.Zero))
				return null;
			uint hashCode = CoreInternals.Object_GetHashCode(handle);
			return TryBindReferenceHolder(handle, (handler) => Wrap(hashCode, handler));
		}

		public IActor GetOwner(IComponent component)
		{
			Handler ptr = ActorInternals.Node_GetScene(GetPointerFromObj(component));
			return RootDriver.ActorDriver.Wrap(ptr);
		}

		public IWorld GetWorld(IComponent component)
		{
			Handler ptr = ActorInternals.Node_GetScene(GetPointerFromObj(component));
			return RootDriver.WorldDriver.Wrap(ptr);
		}
	}
}
