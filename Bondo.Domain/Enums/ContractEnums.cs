namespace Bondo.Domain.Enums;
public class ContractEnums
{
    public enum Visibility{
        Public = 0,
        Private = 1
    }
    public enum DisbursementOption{
        NGN = 0,
        Crypto = 1
    }
    public enum ContractStatus{
        InActive = 0,
        Active = 1,
        Deleted = 2
    }
    public enum ApplicationStatus{
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
