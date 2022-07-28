namespace Trading.Extensions;

public class WalletUpdateException : Exception
{
	public WalletUpdateException(string message, Exception inner)
		: base(message, inner) { }
}