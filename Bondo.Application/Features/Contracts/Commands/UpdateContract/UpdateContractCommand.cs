using AutoMapper;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Application;
public record UpdateContractCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        // public string OwnerUserId { get; set; }
        public string? ContractorUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ContractValue { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ContractEnums.Visibility Visibility { get; set; } 
        public ContractEnums.DisbursementOption DisbursementOption { get; set; } 
        public ContractEnums.ContractStatus Status { get; set; } 
        public string PaymentTerms { get; set; }
        public string AgreementUrl { get; set; }
    }

    internal class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContractCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }

        public async Task<Result<int>> Handle(UpdateContractCommand command, CancellationToken cancellationToken)
        {
            var Contract = await _unitOfWork.Repository<Contract>().GetByIdAsync(command.Id);
            if (Contract != null)
            {
                Contract.ContractorUserId = command.ContractorUserId;
                Contract.Title = command.Title;
                Contract.Description = command.Description;
                Contract.Visibility = command.Visibility;
                Contract.DisbursementOption = command.DisbursementOption;
                Contract.Status = command.Status;
                Contract.PaymentTerms = command.PaymentTerms;
                Contract.AgreementUrl = command.AgreementUrl;

                await _unitOfWork.Repository<Contract>().UpdateAsync(Contract);
                Contract.AddDomainEvent(new ContractUpdatedEvent(Contract));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(Contract.Id, "Contract Updated.");
            }
            else
            {
                return await Result<int>.FailureAsync("Contract Not Found.");
            }               
        }
    }