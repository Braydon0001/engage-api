namespace Engage.Application.Services.TrainingCategories.Commands;

public class CreateTrainingCategoryCommand : TrainingCategoryCommand, IRequest<OperationStatus>
{
}

public class CreateTrainingCategoryCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingCategoryCommand, OperationStatus>
{
    public CreateTrainingCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingCategoryCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateTrainingCategoryCommand, TrainingCategory>(command);
        _context.TrainingCategories.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingCategoryId;
        return opStatus;
    }
}
