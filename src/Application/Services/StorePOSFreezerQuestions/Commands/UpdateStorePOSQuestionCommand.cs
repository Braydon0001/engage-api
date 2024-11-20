namespace Engage.Application.Services.StorePOSFreezerQuestions.Commands;

public class UpdateStorePOSQuestionCommand : StorePOSQuestionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateStorePOSQuestionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStorePOSQuestionCommand, OperationStatus>
{
    public UpdateStorePOSQuestionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateStorePOSQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOSQuestions.SingleAsync(x => x.StorePOSQuestionId == request.Id);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSQuestionId;
        return opStatus;
    }
}
