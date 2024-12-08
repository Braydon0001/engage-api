﻿namespace Engage.Application.Services.FileContainers.Models;

public class FileContainerVm : IMapFrom<FileContainer>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContainerName { get; set; }
    public bool PublicAccess { get; set; }
    public string FileNameStrategy { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileContainer, FileContainerVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FileContainerId));
    }
}
