using Api.Entities;
using AutoMapper;

namespace Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Configure AutoMapper.
            CreateMap<Person, PersonDtoRead>();
            CreateMap<PersonDtoCreateUpdate, Person>();
        }
    }
}
