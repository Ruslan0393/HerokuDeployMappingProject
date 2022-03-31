using AutoMapper;
using Mapping.Domain.Entities;
using Mapping.ViewModel.User;

namespace Mapping.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateViewModel, User>().ReverseMap();
        }
    }
}
