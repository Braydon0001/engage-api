namespace Engage.Application.Services.CreditorCutOffSettings.Commands;

public class CreditorCutOffSettingInsertCommand : IMapTo<CreditorCutOffSetting>, IRequest<CreditorCutOffSetting>
{
    public string CreditorCutOff { get; set; }
    public string PaymentCutOff { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorCutOffSettingInsertCommand, CreditorCutOffSetting>();
    }
}

public record CreditorCutOffSettingInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorCutOffSettingInsertCommand, CreditorCutOffSetting>
{
    public async Task<CreditorCutOffSetting> Handle(CreditorCutOffSettingInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorCutOffSettingInsertCommand, CreditorCutOffSetting>(command);

        Context.CreditorCutOffSettings.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CreditorCutOffSettingInsertValidator : AbstractValidator<CreditorCutOffSettingInsertCommand>
{
    public CreditorCutOffSettingInsertValidator()
    {
        RuleFor(e => e.CreditorCutOff).NotEmpty().MaximumLength(30);
        RuleFor(e => e.PaymentCutOff).NotEmpty().MaximumLength(30);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}