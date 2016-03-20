using System;

namespace Domain
{
	public class KeyInUseException : Exception
	{
		public KeyInUseException(string key)
			: base($"The key '{key}' is already in use.")
		{
		}
	}
}
