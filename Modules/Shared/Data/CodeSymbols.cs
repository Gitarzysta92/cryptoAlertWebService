namespace Shared.Data;

public enum CoinCodes
{
	Eth = 1,
	Btc = 2,
	Sol = 3,
	Ada = 4,
	Ape = 5
}

public enum FiatCodes
{
	Usd = 1
}

public static class CodeSymbols
{
	public static IList<string> GetSymbols()
	{
		var coinCodes = Enum.GetNames(typeof(CoinCodes)).Select(c => c.ToUpper());
		var fiatCodes = Enum.GetNames(typeof(FiatCodes)).Select(c => c.ToUpper());

		var codes = new List<string>();

		foreach (var coinCode in coinCodes)
			foreach (var fiatCode in fiatCodes)
				codes.Add($"{coinCode}-{fiatCode}");

		return codes;
	}
}