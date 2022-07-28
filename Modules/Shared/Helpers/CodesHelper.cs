namespace Shared.Helpers;

public static class CodesHelper
{
	public static string GetFiatShortNameFromCode(string code)
	{
		return code.Split('-')[1];
	}

	public static string GetCoinShortNameFromCode(string code)
	{
		return code.Split('-')[0];
	}

	public static string NormalizeCode(string code)
	{
		return code.ToUpper();
	}
}