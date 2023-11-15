using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Application.Features.Contracts.Commands;
public class ContractDeletedEvent : BaseEvent
    {
        public Contract Contract { get; }

        public ContractDeletedEvent(Contract contract)
        {
            Contract = contract;
        }
    }
