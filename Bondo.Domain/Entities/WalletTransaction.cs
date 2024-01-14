using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Domain.Entities;
public class WalletTransaction
{
    public string Id { get; set; }
    public string WalletId { get; set; }
    public string RecepientWalletId { get; set; }
    public decimal Amount { get; set; }
    public string InitiatorDetails { get; set; }
    public string Description { get; set; }
    public WalletEnums.TransactionType Type { get; set; } //credit, debit
    public WalletEnums.TransactionStatus Status { get; set; } // pending, succeeded
    public Wallet Wallet { get; set; }
}
