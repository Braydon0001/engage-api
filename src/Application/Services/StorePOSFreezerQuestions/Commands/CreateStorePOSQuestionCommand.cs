namespace Engage.Application.Services.StorePOSFreezerQuestions.Commands;

public class CreateStorePOSQuestionCommand : StorePOSQuestionCommand, IRequest<OperationStatus>
{

}

public class CreateStorePOSQuestionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStorePOSQuestionCommand, OperationStatus>
{
    public CreateStorePOSQuestionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateStorePOSQuestionCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateStorePOSQuestionCommand, StorePOSQuestion>(request);
        _context.StorePOSQuestions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSQuestionId;
        return opStatus;
    }
}
