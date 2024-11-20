// auto-generated
namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionNextUpdateCommand : IMapTo<EmployeePension>, IRequest<EmployeePension>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public IFormFile[] FileDeathNominationForm { get; set; }
    public IFormFile[] FileFuneralCoverForm { get; set; }
    public IFormFile[] FileInvestmentForm { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePensionNextUpdateCommand, EmployeePension>();
    }
}

public class EmployeePensionNextUpdateHandler : UpdateHandler, IRequestHandler<EmployeePensionNextUpdateCommand, EmployeePension>
{
    protected readonly IFileService _file;
    public EmployeePensionNextUpdateHandler(IAppDbContext context, IMapper mapper, IFileService file) : base(context, mapper)
    {
        _file = file;
    }

    public async Task<EmployeePension> Handle(EmployeePensionNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePensions.SingleOrDefaultAsync(e => e.EmployeePensionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        var deathNominationFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = mappedEntity.Files,
            MaxFiles = 5,
            Files = command.FileDeathNominationForm,
            Id = command.Id,
            FileType = "deathNominationForm"
        };

        mappedEntity.Files = await _file.UpdateAsync(deathNominationFormFileUpdateCommand, cancellationToken);

        var funeralCoverFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = mappedEntity.Files,
            MaxFiles = 5,
            Files = command.FileFuneralCoverForm,
            Id = command.Id,
            FileType = "funeralCoverForm"
        };

        mappedEntity.Files = await _file.UpdateAsync(funeralCoverFormFileUpdateCommand, cancellationToken);

        var investmentFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = mappedEntity.Files,
            MaxFiles = 5,
            Files = command.FileInvestmentForm,
            Id = command.Id,
            FileType = "investmentForm"
        };

        mappedEntity.Files = await _file.UpdateAsync(investmentFormFileUpdateCommand, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeePensionNextValidator : AbstractValidator<EmployeePensionNextUpdateCommand>
{
    public UpdateEmployeePensionNextValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionSchemeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionContributionPercentageId).NotEmpty().GreaterThan(0);
    }
}