using Database.Models;

namespace Trading.Models;

public class WalletDto
{
	public string Id { get; set; }
	
	public Guid UserId { get; set; }
	public List<FiatWalletBalance> FiatBalance { get; set; } = new List<FiatWalletBalance>{};
	public List<CoinWalletBalance> CryptoBalance { get; set; } = new List<CoinWalletBalance>{};
}