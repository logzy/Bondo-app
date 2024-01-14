namespace Bondo.Domain.Enums;
public class WalletEnums
{
    public enum WalletType{
        FIAT = 1,
        CRYPTO = 2
    }
    public enum WalletStatus{
        Active = 1,
        Diasabled = 2,
        Deleted = 3,
        Flagged = 4
    }
    public enum TransactionStatus{
        Pending = 0,
        Succeeded = 1,
        Failed = 2,
        Hold = 3
    }
    public enum TransactionType{
        Credit = 0,
        Debit =  1
    }
}
