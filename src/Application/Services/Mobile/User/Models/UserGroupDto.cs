using Engage.Application.Services.UserUserGroups.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Application.Services.Mobile.User.Models
{
    public class UserGroupDto : IMapFrom<UserUserGroup>
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserUserGroup, UserGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserUserGroupId));

        }
    }
}
