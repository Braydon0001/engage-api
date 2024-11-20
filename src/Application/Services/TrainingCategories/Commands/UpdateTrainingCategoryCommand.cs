namespace Engage.Application.Services.TrainingCategories.Commands;

public class UpdateTrainingCategoryCommand : TrainingCategoryCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateTrainingCategoryCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingCategoryCommand, OperationStatus>
{
    public UpdateTrainingCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateTrainingCategoryCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingCategories.SingleAsync(x => x.TrainingCategoryId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingCategoryId;
        return opStatus;
    }
}
