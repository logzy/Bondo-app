using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application.Features.Contracts.Commands;
public class ContractApplicationUpdatedEvent : BaseEvent
{
    public ContractApplication ContractApplication { get; }

    public ContractApplicationUpdatedEvent(ContractApplication application)
    {
        ContractApplication = application;
    }
}
