namespace Engage.Application.Services.CreditorCutOffSettings.Commands;

public class CreditorCutOffSettingUpdateCommand : IMapTo<CreditorCutOffSetting>, IRequest<CreditorCutOffSetting>
{
    public int Id { get; set; }
    public string CreditorCutOff { get; set; }
    public string PaymentCutOff { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorCutOffSettingUpdateCommand, CreditorCutOffSetting>();
    }
}

public record CreditorCutOffSettingUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorCutOffSettingUpdateCommand, CreditorCutOffSetting>
{
    public async Task<CreditorCutOffSetting> Handle(CreditorCutOffSettingUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorCutOffSettings.SingleOrDefaultAsync(e => e.CreditorCutOffSettingId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorCutOffSettingValidator : AbstractValidator<CreditorCutOffSettingUpdateCommand>
{
    public UpdateCreditorCutOffSettingValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorCutOff).NotEmpty().MaximumLength(30);
        RuleFor(e => e.PaymentCutOff).NotEmpty().MaximumLength(30);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}