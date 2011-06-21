using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Colourblind.Core
{
	public class Singleton<S> where S : new()
	{
		private static Mutex _mutex = new Mutex();
		private static S _instance;

		public static S Instance
		{
			get
			{
				_mutex.WaitOne();
				if (_instance == null)
					_instance = new S();
				_mutex.ReleaseMutex();
				return _instance;
			}
		}
	}
}
