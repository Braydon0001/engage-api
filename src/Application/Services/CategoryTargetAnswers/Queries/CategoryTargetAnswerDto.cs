namespace Engage.Application.Services.CategoryTargetAnswers.Queries;

public class CategoryTargetAnswerDto : IMapFrom<CategoryTargetAnswer>
{
    public int CategoryTargetAnswerId { get; init; }
    public int CategoryTargetId { get; init; }
    public float? Target { get; init; }
    public float? Available { get; init; }
    public float? Occupied { get; init; }
    public bool IsNotApplicable { get; init; }
    public string TextAnswer { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetAnswer, CategoryTargetAnswerDto>();
    }
}
