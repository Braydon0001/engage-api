using Engage.Application.Services.SurveyQuestions.Commands;

namespace Engage.Application.Services.SurveyQuestionRules.Commands
{
    public class QuestionSurveyRuleUpsertCommand : IRequest<OperationStatus>, IMapTo<SurveyQuestionRule>
    {
        public int SurveyQuestionRuleId { get; set; }
        public int QuestionId { get; set; }
        public int TargetQuestionId { get; set; }
        public string Operation { get; set; }
        public int RuleIndex { get; set; }
        public string RuleText { get; set; }
        public string Value { get; set; }
        public SurveyQuestionRuleValueType ValueType { get; set; }
        public SurveyQuestionRuleType RuleType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<QuestionSurveyRuleUpsertCommand, SurveyQuestionRule>();
        }
    }

    public class QuestionSurveyRuleUpsertCommandHandler : BaseCreateCommandHandler, IRequestHandler<QuestionSurveyRuleUpsertCommand, OperationStatus>
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;

        public QuestionSurveyRuleUpsertCommandHandler(IAppDbContext _context, IMapper _mapper) : base(_context, _mapper)
        {
            context = _context;
            mapper = _mapper;
        }


        public async Task<OperationStatus> Handle(QuestionSurveyRuleUpsertCommand command, CancellationToken cancellationToken)
        {
            if (command.SurveyQuestionRuleId == 0)
            {
                var entity = mapper.Map<QuestionSurveyRuleUpsertCommand, SurveyQuestionRule>(command);
                context.SurveyQuestionRules.Add(entity);
                var opStatus = await context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.SurveyQuestionRuleId;
                return opStatus;
            }
            else
            {
                var entity = await context.SurveyQuestionRules.FindAsync(command.SurveyQuestionRuleId);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(SurveyQuestionRule), command.SurveyQuestionRuleId);
                }
                mapper.Map(command, entity);
                return await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
