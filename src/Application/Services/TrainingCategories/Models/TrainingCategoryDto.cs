namespace Engage.Application.Services.TrainingCategories.Models;

public class TrainingCategoryDto : IMapFrom<TrainingCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingCategory, TrainingCategoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingCategoryId));
    }
}
