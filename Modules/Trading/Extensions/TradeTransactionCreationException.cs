namespace Trading.Extensions;

public class TradeTransactionCreationException : Exception
{
	public TradeTransactionCreationException(string message, Exception inner)
		: base(message, inner) { }
}