using Bondo.Domain.Common;
using Bondo.Domain.Enums;

namespace Bondo.Domain.Entities;
public class ContractApplication : BaseAuditableEntity
{
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    public string ContractorId { get; set; }
    public ContractEnums.ApplicationStatus Status { get; set; } 
    public string AdditionalNotes { get; set; }

}
