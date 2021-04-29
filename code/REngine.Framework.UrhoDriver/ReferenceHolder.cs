using System;

namespace REngine.Framework.UrhoDriver
{
	internal sealed class ReferenceHolder
	{

		public object StrongRef { get; private set; } = null;
		public WeakReference<object> WeakRef { get; private set; }

		public object Target
		{
			get
			{
				object obj;
				if (IsStrong)
					obj = StrongRef;
				else
					WeakRef.TryGetTarget(out obj);

				return obj;
			}
		}

		public bool IsStrong
		{
			get => StrongRef != null;
		}

		public bool IsWeak
		{
			get
			{
				return !IsStrong;
			}
		}

		public ReferenceHolder(object obj)
		{
			WeakRef = new WeakReference<object>(obj);
		}

		public void MakeStrong()
		{
			object refObj;
			WeakRef.TryGetTarget(out refObj);

			StrongRef = refObj;
		}

		public void MakeWeak()
		{
			StrongRef = null;
		}
	}
}
