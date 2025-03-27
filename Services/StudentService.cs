using AutoMapper;
using GraphQLPractive.Model;
using GraphQLPractive.DTO;
using GraphQLPractive.Exceptions;
using GraphQLPractive.Repositories;

namespace GraphQLPractive.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found");
            return _mapper.Map<StudentDto>(student);
        }
        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto
        createStudentDto)
        {
        var student = _mapper.Map<Student>(createStudentDto);
            var createdStudent = await _repository.CreateAsync(student);
            return _mapper.Map<StudentDto>(createdStudent);
        }
        public async Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto
        updateStudentDto)
        {
            var existingStudent = await _repository.GetByIdAsync(id);
            if (existingStudent == null)
                throw new NotFoundException($"Student with ID {id} not found");
            _mapper.Map(updateStudentDto, existingStudent);
            var updatedStudent = await _repository.UpdateAsync(existingStudent);
            return _mapper.Map<StudentDto>(updatedStudent);
        }
        public async Task DeleteStudentAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                throw new NotFoundException($"Student with ID {id} not found");
            await _repository.DeleteAsync(id);
        }
    }
}
