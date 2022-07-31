using AutoMapper;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;
using StudentForum.WebUI.Models.Account;
using StudentForum.WebUI.Models.Manage;

namespace StudentForum.WebUI.MapperConfig
{
    internal class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<RegisteModelView, RegisterDto>()
                .ForMember(x => x.Photo, option => option.MapFrom(x => x.CoverPhotoBytes));
            CreateMap<LoginModelView, LoginDto>();

            CreateMap<User, ProfileModelView>();

            CreateMap<UserUpdateModelView, UserDto>();
            CreateMap<User, UserUpdateModelView>();

            CreateMap<ChangePasswordModelView, ChangePasswordDto>();

        }
    }
}
