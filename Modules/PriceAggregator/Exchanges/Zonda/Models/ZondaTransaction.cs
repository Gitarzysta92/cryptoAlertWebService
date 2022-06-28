namespace PriceAggregator.Zonda.Models;

public class ZondaResponseDto
{
	public string Action { get; set; } = null!;
	public string Topic { get; set; } = null!;
	public ZondaTradingTickerMessage Message { get; set; } = null!;
	public string Timestamp { get; set; } = null!;
	public int SeqNo { get; set; }
}

public class ZondaTradingTickerMessage
{
	public ZondaMarket Market { get; set; } = null!;
	public string Time { get; set; } = null!;
	public string Rate { get; set; } = null!;
	public string PrevRate { get; set; } = null!;
}

public class ZondaMarket
{
	public string Code { get; set; } = null!;
}