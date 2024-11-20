using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using Engage.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.ClaimStatusUsers.Commands
{
    public class CreateClaimStatusUserCommand : ClaimStatusUserCommand, IRequest<OperationStatus>
    {
        
    }

    public class CreateClaimStatusUserCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimStatusUserCommand, OperationStatus>
    {
        public CreateClaimStatusUserCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateClaimStatusUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateClaimStatusUserCommand, ClaimStatusUser>(request);
            _context.ClaimStatusUsers.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimStatusUserId;
            return opStatus;
        }
    }
}
