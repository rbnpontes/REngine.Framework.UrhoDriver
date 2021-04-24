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

		public (uint UnAllocatedCount, uint AliveCount) GetStatus()
		{
			uint unalloc = 0;
			uint alive = 0;

#if DEBUG
			MemoryTracerInternals.MemoryTracer_GetObjectStatus(out unalloc, out alive);
#endif
			return (unalloc, alive);
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
