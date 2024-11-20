namespace Engage.Application.Services.TrainingCategories.Commands;

public class TrainingCategoryCommand : IMapTo<TrainingCategory>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingCategoryCommand, TrainingCategory>();
    }
}
