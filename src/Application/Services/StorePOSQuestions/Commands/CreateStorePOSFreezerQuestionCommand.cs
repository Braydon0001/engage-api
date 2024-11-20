namespace Engage.Application.Services.StorePOSQuestions.Commands;

public class CreateStorePOSFreezerQuestionCommand : StorePOSFreezerQuestionCommand, IRequest<OperationStatus>
{

}

public class CreateStorePOSFreezerQuestionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStorePOSFreezerQuestionCommand, OperationStatus>
{
    public CreateStorePOSFreezerQuestionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateStorePOSFreezerQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateStorePOSFreezerQuestionCommand, StorePOSFreezerQuestion>(request);
        _context.StorePOSFreezerQuestions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSFreezerQuestionId;
        return opStatus;
    }
}
