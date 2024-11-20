namespace Engage.Application.Services.StorePOSQuestions.Commands;

public class UpdateStorePOSFreezerQuestionCommand : StorePOSFreezerQuestionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateStorePOSFreezerQuestionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStorePOSFreezerQuestionCommand, OperationStatus>
{
    public UpdateStorePOSFreezerQuestionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateStorePOSFreezerQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOSFreezerQuestions.SingleAsync(x => x.StorePOSFreezerQuestionId == request.Id);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSFreezerQuestionId;
        return opStatus;
    }
}
