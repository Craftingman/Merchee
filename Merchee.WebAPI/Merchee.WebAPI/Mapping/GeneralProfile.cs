using AutoMapper;
using Merchee.Domain.Entities;

namespace Merchee.WebAPI.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Company, Company>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Shelves, opt => opt.Ignore())
                .ForMember(c => c.Products, opt => opt.Ignore())
                .ForMember(c => c.Users, opt => opt.Ignore());
        }
    }
}
