using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeePersonalCommand : IMapTo<Employee>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? EmployeeLanguageId { get; set; }
    public int? EmployeeGenderId { get; set; }
    public int? EmployeeRaceId { get; set; }
    public int? EmployeeNationalityId { get; set; }
    public int EmployeeCitzenshipId { get; set; }
    public int? EmployeeDisabledTypeId { get; set; }
    public int? MaritalStatusId { get; set; }
    public int? UniformSizeId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsForeignNational { get; set; }
    public bool IsCovidVaccinated { get; set; }

    public List<int> EmployeeHealthConditionIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateEmployeePersonalCommand, Employee>()
            .ForMember(dest => dest.EmployeeDisabledTypeId, opt => opt.MapFrom(
                       src => src.EmployeeDisabledTypeId.HasValue && src.EmployeeDisabledTypeId == 0 ? null : src.EmployeeDisabledTypeId))
            .ForMember(dest => dest.EmployeeLanguageId, opt => opt.MapFrom(
                       src => src.EmployeeLanguageId.HasValue && src.EmployeeLanguageId == 0 ? null : src.EmployeeLanguageId))
            .ForMember(dest => dest.EmployeeNationalityId, opt => opt.MapFrom(
                       src => src.EmployeeNationalityId.HasValue && src.EmployeeNationalityId == 0 ? null : src.EmployeeNationalityId))
            .ForMember(dest => dest.EmployeeRaceId, opt => opt.MapFrom(
                       src => src.EmployeeRaceId.HasValue && src.EmployeeRaceId == 0 ? null : src.EmployeeRaceId))
            .ForMember(dest => dest.EmployeeGenderId, opt => opt.MapFrom(
                       src => src.EmployeeGenderId.HasValue && src.EmployeeGenderId == 0 ? null : src.EmployeeGenderId))
            .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(
                       src => src.MaritalStatusId.HasValue && src.MaritalStatusId == 0 ? null : src.MaritalStatusId))
            .ForMember(dest => dest.UniformSizeId, opt => opt.MapFrom(
                       src => src.UniformSizeId.HasValue && src.UniformSizeId == 0 ? null : src.UniformSizeId));
    }
}

public class UpdateEmployeePersonalCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeePersonalCommand, OperationStatus>
{
    public UpdateEmployeePersonalCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeePersonalCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(e => e.EmployeeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.EmployeeHealthConditionIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(
                AssignDesc.HEALTH_CONDITION_EMPLOYEE, entity.EmployeeId, command.EmployeeHealthConditionIds), cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}

public class UpdateEmployeePersonalValidator : AbstractValidator<UpdateEmployeePersonalCommand>
{
    public UpdateEmployeePersonalValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
