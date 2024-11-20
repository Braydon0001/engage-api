namespace Engage.Application.Services.EmployeeFiles.Commands;

public class EmployeeFileUpdateCommand : IMapTo<EmployeeFile>, IRequest<EmployeeFile>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileUpdateCommand, EmployeeFile>();
    }
}

public class EmployeeFileUpdateHandler : UpdateHandler, IRequestHandler<EmployeeFileUpdateCommand, EmployeeFile>
{
    public EmployeeFileUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFile> Handle(EmployeeFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFiles.SingleOrDefaultAsync(e => e.EmployeeFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeFileValidator : AbstractValidator<EmployeeFileUpdateCommand>
{
    public UpdateEmployeeFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeFileTypeId).NotEmpty().GreaterThan(0);
    }
}