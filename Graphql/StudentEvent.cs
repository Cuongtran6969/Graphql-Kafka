using GraphQLPractive.DTO;

namespace GraphQLPractive.Graphql
{
    public class StudentEvent
    {
        public event EventHandler<StudentDto> StudentCreated;
        public event EventHandler<StudentDto> StudentUpdated;
        public event EventHandler<int> StudentDeleted;

        public void OnStudentCreated(StudentDto student)
        {
            StudentCreated?.Invoke(this, student);
        }

        public void OnStudentUpdated(StudentDto student)
        {
            StudentUpdated?.Invoke(this, student);
        }

        public void OnStudentDeleted(int studentId)
        {
            StudentDeleted?.Invoke(this, studentId);
        }
    }
}
