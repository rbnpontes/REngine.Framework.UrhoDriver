using REngine.Framework.Drivers;
using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver.Drivers
{
	internal enum ResourceType
	{
		Model,
		Material,
		Image,
		Animation,
		Shader,
		Sound,
		Texture2D,
		Texture2DArray,
		Texture3D,
		TextureCube,
		ParticleEffect,
		JSONFile,
		XMLFile
	}

	internal class ResourceManagerDriver : BaseDriver
	{
		public ResourceManagerDriver(RootDriver driver) : base(driver)
		{
		}

		public Handler GetResourceCacheHandle()
		{
			ValidateThread();
			Handler handler = ResourceCacheInternals.ResourceCache_Get(RootDriver.ContextPtr);
			return handler;
		}
		
		public Handler LoadResource(ResourceType type, IHandle resourceCache, string name)
		{
			ValidateThread();
			Func<IntPtr, string, IntPtr> call = (x, y) => IntPtr.Zero;
			switch (type)
			{
				case ResourceType.Animation:
					break;
				case ResourceType.Image:
					break;
				case ResourceType.Model:
					call = ResourceCacheInternals.ResourceCache_GetModel;
					break;
				case ResourceType.Material:
					break;
				case ResourceType.Texture2D:
					break;
				case ResourceType.Texture3D:
					break;
				case ResourceType.Texture2DArray:
					break;
				case ResourceType.TextureCube:
					break;
				case ResourceType.Sound:
					break;
				case ResourceType.Shader:
					break;
				case ResourceType.JSONFile:
					break;
				case ResourceType.XMLFile:
					break;
				case ResourceType.ParticleEffect:
					break;
			}

			Handler result = call((IntPtr)resourceCache.Obj, name);
			return result;
		}
	}
}
