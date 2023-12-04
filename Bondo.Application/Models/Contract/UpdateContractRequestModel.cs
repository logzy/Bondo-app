using Bondo.Domain.Enums;

namespace Bondo.Application.Models.Contract;
public class UpdateContractRequestModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal ContractValue { get; set; }
    public DateTime DeliveryDate { get; set; }
    public ContractEnums.DisbursementOption DisbursementOption { get; set; }
    public string PaymentTerms { get; set; }
    public string AgreementUrl { get; set; }
}
