using System;

namespace PartyCli.Contracts.Exceptions
{
	public class PartyCliException : Exception
	{
		public PartyCliException(string message) : base(message)
		{
		}
	}
}