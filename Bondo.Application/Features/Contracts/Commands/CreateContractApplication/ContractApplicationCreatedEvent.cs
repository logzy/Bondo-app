using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application.Features.Contracts.Commands;
public class ContractApplicationCreatedEvent : BaseEvent
{
    public ContractApplication ContractApplication { get; }
 
    public ContractApplicationCreatedEvent(ContractApplication application)
    {
        ContractApplication = application;
    }
}