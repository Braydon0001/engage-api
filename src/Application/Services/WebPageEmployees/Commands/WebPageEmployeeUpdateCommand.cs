// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Commands;

public class WebPageEmployeeUpdateCommand : IMapTo<WebPageEmployee>, IRequest<WebPageEmployee>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int WebPageId { get; set; }
    public string ViewDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageEmployeeUpdateCommand, WebPageEmployee>();
    }
}

public class WebPageEmployeeUpdateHandler : UpdateHandler, IRequestHandler<WebPageEmployeeUpdateCommand, WebPageEmployee>
{
    public WebPageEmployeeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPageEmployee> Handle(WebPageEmployeeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebPageEmployees.SingleOrDefaultAsync(e => e.WebPageEmployeeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateWebPageEmployeeValidator : AbstractValidator<WebPageEmployeeUpdateCommand>
{
    public UpdateWebPageEmployeeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.WebPageId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ViewDate).NotEmpty();
    }
}