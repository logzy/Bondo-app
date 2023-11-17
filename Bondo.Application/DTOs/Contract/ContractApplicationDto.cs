using Bondo.Domain.Enums;
using Bondo.Domain.Entities;
using Bondo.Application.Common.Mappings;

namespace Bondo.Application.DTOs.Contract;
public class ContractApplicationDto : IMapFrom<ApplicationUser>
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public Bondo.Domain.Entities.Contract Contract { get; set; }
    public string ContractorId { get; set; }
    public ContractEnums.ApplicationStatus Status { get; set; } 
    public string AdditionalNotes { get; set; }
}
