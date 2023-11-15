using Bondo.Application.Common.Mappings;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Application.DTOs.Contract;
public class ContractDto : IMapFrom<Domain.Entities.Contract>
{
    public int Id { get; set; }
    public string OwnerUserId { get; set; }
    public string? ContractorUserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal ContractValue { get; set; }
    public DateTime DeliveryDate { get; set; }
    public ContractEnums.Visibility Visibility { get; set; } 
    public ContractEnums.DisbursementOption DisbursementOption { get; set; } 
    public ContractEnums.ContractStatus Status { get; set; } 
    public string PaymentTerms { get; set; }
    public string AgreementUrl { get; set; }
}
