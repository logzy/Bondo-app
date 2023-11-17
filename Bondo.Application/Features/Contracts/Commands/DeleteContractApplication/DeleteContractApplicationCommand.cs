using AutoMapper;
using Bondo.Application.Common.Mappings;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Commands;
public record DeleteContractApplicationCommand : IRequest<Result<int>>, IMapFrom<ContractApplication>
{
    public int Id { get; set; }

    public DeleteContractApplicationCommand()
    {

    }

    public DeleteContractApplicationCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteContractApplicationCommandHandler : IRequestHandler<DeleteContractApplicationCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteContractApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(DeleteContractApplicationCommand command, CancellationToken cancellationToken)
    {
        var contractApplication = await _unitOfWork.Repository<ContractApplication>().GetByIdAsync(command.Id);
        if (contractApplication != null)
        {
            await _unitOfWork.Repository<ContractApplication>().DeleteAsync(contractApplication);
            contractApplication.AddDomainEvent(new ContractApplicationDeletedEvent(contractApplication));

            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(contractApplication.Id, "Contract Application Deleted");
        }
        else
        {
            return await Result<int>.FailureAsync("Contract Application Not Found.");
        }
    }
}
