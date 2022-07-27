using AutoMapper;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.Data.Entities.Account;

namespace StudentForum.BusinessLogic.MapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(x => x.UserName, option => option.MapFrom(x => x.Email));
        }
    }
}
