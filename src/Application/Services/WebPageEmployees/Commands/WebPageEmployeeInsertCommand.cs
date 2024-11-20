// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Commands;

public class WebPageEmployeeInsertCommand : IMapTo<WebPageEmployee>, IRequest<WebPageEmployee>
{
    public int EmployeeId { get; set; }
    public int WebPageId { get; set; }
    public DateTime ViewDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageEmployeeInsertCommand, WebPageEmployee>();
    }
}

public class WebPageEmployeeInsertHandler : InsertHandler, IRequestHandler<WebPageEmployeeInsertCommand, WebPageEmployee>
{
    public WebPageEmployeeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPageEmployee> Handle(WebPageEmployeeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<WebPageEmployeeInsertCommand, WebPageEmployee>(command);
        
        _context.WebPageEmployees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class WebPageEmployeeInsertValidator : AbstractValidator<WebPageEmployeeInsertCommand>
{
    public WebPageEmployeeInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.WebPageId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ViewDate).NotEmpty();
    }
}