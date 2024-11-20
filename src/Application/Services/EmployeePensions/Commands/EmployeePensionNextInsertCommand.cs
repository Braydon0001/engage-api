// auto-generated

namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionNextInsertCommand : IMapTo<EmployeePension>, IRequest<EmployeePension>
{
    public int EmployeeId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public IFormFile[] FileDeathNominationForm { get; set; }
    public IFormFile[] FileFuneralCoverForm { get; set; }
    public IFormFile[] FileInvestmentForm { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePensionNextInsertCommand, EmployeePension>();
    }
}

public class EmployeePensionNextInsertHandler : InsertHandler, IRequestHandler<EmployeePensionNextInsertCommand, EmployeePension>
{
    protected readonly IFileService _file;
    public EmployeePensionNextInsertHandler(IAppDbContext context, IMapper mapper, IFileService file) : base(context, mapper)
    {
        _file = file;
    }

    public async Task<EmployeePension> Handle(EmployeePensionNextInsertCommand command, CancellationToken cancellationToken)
    {
        var existingPension = await _context.EmployeePensions
                                            .Where(s => s.EmployeeId == command.EmployeeId && s.Disabled == false)
                                            .FirstOrDefaultAsync(cancellationToken);

        if (existingPension != null)
        {
            throw new Exception("Employee Already has Pension. Please update the existing one.");
        }

        var entity = _mapper.Map<EmployeePensionNextInsertCommand, EmployeePension>(command);

        _context.EmployeePensions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var deathNominationFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            Files = command.FileDeathNominationForm,
            Id = entity.EmployeePensionId,
            FileType = "deathNominationForm"
        };

        entity.Files = await _file.UpdateAsync(deathNominationFormFileUpdateCommand, cancellationToken);

        var funeralCoverFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            Files = command.FileFuneralCoverForm,
            Id = entity.EmployeePensionId,
            FileType = "funeralCoverForm"
        };

        entity.Files = await _file.UpdateAsync(funeralCoverFormFileUpdateCommand, cancellationToken);

        var investmentFormFileUpdateCommand = new FileUpdateCommand
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            Files = command.FileInvestmentForm,
            Id = entity.EmployeePensionId,
            FileType = "investmentForm"
        };

        entity.Files = await _file.UpdateAsync(investmentFormFileUpdateCommand, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeePensionNextInsertValidator : AbstractValidator<EmployeePensionNextInsertCommand>
{
    public EmployeePensionNextInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionSchemeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionContributionPercentageId).NotEmpty().GreaterThan(0);
    }
}