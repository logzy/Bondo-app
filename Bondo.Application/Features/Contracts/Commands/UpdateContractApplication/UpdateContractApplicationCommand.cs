using AutoMapper;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Application.Features.Contracts.Commands;

public record UpdateContractApplicationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public ContractEnums.ApplicationStatus Status { get; set; } 
        public string AdditionalNotes { get; set; }
    }

    internal class UpdateContractApplicationCommandHandler : IRequestHandler<UpdateContractApplicationCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContractApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }

        public async Task<Result<int>> Handle(UpdateContractApplicationCommand command, CancellationToken cancellationToken)
        {
            var contractApplication = await _unitOfWork.Repository<ContractApplication>().GetByIdAsync(command.Id);
            if (contractApplication != null)
            {
                contractApplication.Status = command.Status;
                contractApplication.AdditionalNotes = command.AdditionalNotes;
                await _unitOfWork.Repository<ContractApplication>().UpdateAsync(contractApplication);
                contractApplication.AddDomainEvent(new ContractApplicationUpdatedEvent(contractApplication));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(contractApplication.Id, "Contract Application Updated.");
            }
            else
            {
                return await Result<int>.FailureAsync("Contract Application Not Found.");
            }               
        }
    }