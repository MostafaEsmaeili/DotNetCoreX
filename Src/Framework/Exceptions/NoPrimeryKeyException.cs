namespace Framework.Exceptions
{
	public class NoPkException : System.Exception
	{
		public NoPkException()
		{
		}

		public NoPkException(string message)
			: base(message)
		{
		}

		public NoPkException(string message, System.Exception inner)
			: base(message, inner)
		{
		}
	}
}