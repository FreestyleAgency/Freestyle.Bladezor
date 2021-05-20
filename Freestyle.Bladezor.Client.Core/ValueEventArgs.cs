using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Bladezor.Client
{
	public class ValueEventArgs<T> : EventArgs
	{
		public T Value { get; private set; }

		public ValueEventArgs(T value) {
			Value = value;
		}
	}
}
