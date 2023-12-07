using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bondo.Domain.Common;
using Bondo.Domain.Enums;

namespace Bondo.Domain.Entities;
public class Contract : BaseAuditableEntity
{
    public string OwnerUserId { get; set; }
    public string? ContractorUserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
    public decimal ContractValue { get; set; }
    public DateTime DeliveryDate { get; set; }
    public ContractEnums.Visibility Visibility { get; set; } 
    public ContractEnums.DisbursementOption DisbursementOption { get; set; } 
    public ContractEnums.ContractStatus Status { get; set; } 
    public string PaymentTerms { get; set; }
    public string AgreementUrl { get; set; }

}
