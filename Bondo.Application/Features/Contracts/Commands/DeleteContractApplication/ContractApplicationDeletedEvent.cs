using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application.Features.Contracts.Commands;
public class ContractApplicationDeletedEvent : BaseEvent
{
    public ContractApplication ContractApplication { get; }
 
    public ContractApplicationDeletedEvent(ContractApplication application)
    {
        ContractApplication = application;
    }
}