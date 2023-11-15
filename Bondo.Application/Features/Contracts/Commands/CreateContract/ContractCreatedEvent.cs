using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application.Features.Contracts.Commands;
public class ContractCreatedEvent : BaseEvent
{
    public Contract Contract { get; }
 
    public ContractCreatedEvent(Contract contract)
    {
        Contract = contract;
    }
}
