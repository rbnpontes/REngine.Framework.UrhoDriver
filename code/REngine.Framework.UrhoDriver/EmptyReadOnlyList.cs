using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REngine.Framework.UrhoDriver
{
	internal class EmptyReadOnlyList<T> : IReadOnlyList<T>
	{
		public T this[int index] => default(T);

		public int Count => 0;

		public IEnumerator<T> GetEnumerator()
		{
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
