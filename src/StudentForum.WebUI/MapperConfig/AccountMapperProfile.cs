using AutoMapper;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.WebUI.Models.Account;

namespace StudentForum.WebUI.MapperConfig
{
    internal class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<RegisteModelView, RegisterDto>();
            CreateMap<LoginModelView, LoginDto>();
        }
    }
}
