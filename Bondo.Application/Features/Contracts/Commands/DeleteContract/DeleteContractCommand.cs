using AutoMapper;
using Bondo.Application.Common.Mappings;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Commands;
public record DeleteContractCommand : IRequest<Result<int>>, IMapFrom<Contract>
{
    public int Id { get; set; }

    public DeleteContractCommand()
    {

    }

    public DeleteContractCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteContractCommandHandler : IRequestHandler<DeleteContractCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteContractCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(DeleteContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await _unitOfWork.Repository<Contract>().GetByIdAsync(command.Id);
        if (contract != null)
        {
            await _unitOfWork.Repository<Contract>().DeleteAsync(contract);
            contract.AddDomainEvent(new ContractDeletedEvent(contract));

            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(contract.Id, "Contract Deleted");
        }
        else
        {
            return await Result<int>.FailureAsync("Contract Not Found.");
        }
    }
}
