namespace Engage.Application.Services.EngageSubCategories.Commands;

public class UpdateEngageSubCategoryCommand : EngageSubCategoryCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEngageSubCategoryCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageSubCategoryCommand, OperationStatus>
{
    public UpdateEngageSubCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageSubCategories.SingleAsync(e => e.Id == request.Id);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
