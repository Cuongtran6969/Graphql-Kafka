using GraphQLPractive.DTO;

namespace GraphQLPractive.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto);
        Task DeleteStudentAsync(int id);
    }
}
