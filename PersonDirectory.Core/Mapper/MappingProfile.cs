using AutoMapper;
using PersonDirectory.Core.Entities;

namespace PersonDirectory.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, BasePerson>().ForMember(x => x.ID, opt => opt.Ignore());
            CreateMap<BasePerson, Person>().ForMember(x => x.ID, opt => opt.Ignore());
        }
    }
}