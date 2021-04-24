using REngine.Framework.UrhoDriver.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class InternalList<T> : IReadOnlyList<T>
	{
		public T this[int index] => At((uint)index);

		public int Count => (int)ListInternals.ManagedList_GetLength((IntPtr)Handle.Obj);

		public Handler Handle { get; private set; }
		public Func<Handler, T> Getter { get; private set; }

		public InternalList(Handler handler, Func<Handler, T> getter)
		{
			Handle = handler;
			Getter = getter;
		}

		public T At(uint idx)
		{
			Handler targetHandle = ListInternals.ManagedList_Get((IntPtr)Handle.Obj, idx);
			return Getter(targetHandle);
		}

		public IEnumerator<T> GetEnumerator()
		{
			for(uint i=0;i < Count; i++)
			{
				yield return At(i);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
