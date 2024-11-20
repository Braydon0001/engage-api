namespace Engage.Application.Services.SurveyForms.Commands;

public class SurveyFormToggleIsDisabledCommand : IMapTo<SurveyForm>, IRequest<SurveyForm>
{
    public int Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormToggleIsDisabledCommand, SurveyForm>();
    }
}

public record SurveyFormToggleIsDisabledHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormToggleIsDisabledCommand, SurveyForm>
{
    public async Task<SurveyForm> Handle(SurveyFormToggleIsDisabledCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.IsDisabled = !entity.IsDisabled;

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormToggleIsDisabledValidator : AbstractValidator<SurveyFormToggleIsDisabledCommand>
{
    public SurveyFormToggleIsDisabledValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}