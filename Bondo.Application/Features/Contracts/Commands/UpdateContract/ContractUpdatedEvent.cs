using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application;
public class ContractUpdatedEvent : BaseEvent
{
    public Contract Contract { get; }
 
    public ContractUpdatedEvent(Contract contract)
    {
        Contract = contract;
    }
}
