using Bondo.Domain.Common;
using Bondo.Domain.Enums;

namespace Bondo.Domain.Entities;
public class Wallet : BaseAuditableEntity
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public WalletEnums.WalletStatus Status { get; set; } // Active, Disabled
    public WalletEnums.WalletType Type { get; set; } // USD, NGN, crypto
    public ApplicationUser User { get; set; }
}
