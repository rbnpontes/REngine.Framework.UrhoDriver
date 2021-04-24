using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	public sealed class MemoryTracer : IDisposable
	{
		internal MemoryTracer()
		{
		}
		public static MemoryTracer Begin()
		{
			MemoryTracer memoryTracer = new MemoryTracer();
#if DEBUG
			MemoryTracerInternals.MemoryTracer_Begin();
#endif
			return memoryTracer;
		} 
		public void End()
		{
#if DEBUG
			MemoryTracerInternals.MemoryTracer_End();
#endif
		}
		public string GetReport()
		{
#if DEBUG
			return MemoryTracerInternals.MemoryTracer_GetReport();
#else
			return string.Empty;
#endif
		}

		public (uint DeAllocatedCount, uint UnAllocatedCount) GetStatus()
		{
			uint dealloc = 0;
			uint unalloc = 0;

#if DEBUG
			MemoryTracerInternals.MemoryTracer_GetObjectStatus(out dealloc, out unalloc);
#endif
			return (dealloc, unalloc);
		}
		public void Dispose()
		{
			End();
#if DEBUG
			MemoryTracerInternals.MemoryTracer_Clear();
#endif
		}
	}
}
