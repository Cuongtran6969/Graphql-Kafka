using AutoMapper;
using GraphQLPractive.Model;
using GraphQLPractive.DTO;

namespace GraphQLPractive.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember
            != null));
        }
    }
}
