
using Bondo.Domain.Enums;

namespace Bondo.Domain.Entities;
public class WalletTrancastionHistory

{
    public string Id { get; set; }
    public string WalletTransactionId { get; set; }
    public string Event { get; set; }
    public WalletTransaction WalletTransaction { get; set; }
}
