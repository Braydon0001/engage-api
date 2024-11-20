namespace Engage.Application.Services.TrainingCategories.Models;

public class TrainingCategoryVm : IMapFrom<TrainingCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingCategory, TrainingCategoryVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingCategoryId));
    }
}
