﻿namespace Engage.Application.Services.ClaimNotificationUsers.Models;

public class ClaimNotificationUserVm : IMapFrom<ClaimNotificationUser>
{
    public int Id { get; set; }
    public OptionDto ClaimStatusId { get; set; }
    public OptionDto UserId { get; set; }
    public OptionDto EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimNotificationUser, ClaimNotificationUserVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimNotificationUserId))
            .ForMember(d => d.ClaimStatusId, opt => opt.MapFrom(s => new OptionDto(s.ClaimStatus.Id, s.ClaimStatus.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => new OptionDto(s.UserId, $"{s.User.FirstName} {s.User.LastName}")))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)));
    }
}
