namespace Engage.Application.Services.EmployeeFiles.Commands;

public class EmployeeFileInsertCommand : IMapTo<EmployeeFile>, IRequest<EmployeeFile>
{
    public int EmployeeId { get; set; }
    public int EmployeeFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileInsertCommand, EmployeeFile>();
    }
}

public class EmployeeFileInsertHandler : InsertHandler, IRequestHandler<EmployeeFileInsertCommand, EmployeeFile>
{
    public EmployeeFileInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFile> Handle(EmployeeFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeFileInsertCommand, EmployeeFile>(command);

        _context.EmployeeFiles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeFileInsertValidator : AbstractValidator<EmployeeFileInsertCommand>
{
    public EmployeeFileInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeFileTypeId).NotEmpty().GreaterThan(0);
    }
}