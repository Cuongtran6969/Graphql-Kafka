using GraphQL;
using GraphQL.Types;
using GraphQLPractive.Services;
using GraphQLPractive.DTO;
using GraphQLPractive.Kafka;
using GraphQLPractive.Model;

namespace GraphQLPractive.Graphql
{
    public class StudentMutation : ObjectGraphType
    {
        private readonly StudentEvent _studentEvent = new StudentEvent();
        public StudentMutation(IStudentService studentService, KafkaProducerService kafkaProducer)
        {
            //dang ki cac service voi su kien lang nghe khi student create
            _studentEvent.StudentCreated += async (sender, student) =>
            {
                await kafkaProducer.SendMessageAsync(student);
            };

            _studentEvent.StudentUpdated += async (sender, student) =>
            {
                await kafkaProducer.SendMessageAsync(new
                {
                    Status = "Success",
                    Message = $"Updated student: {student.Id}",
                    Data = student,
                    Timestamp = DateTime.UtcNow
                });
            };

            _studentEvent.StudentDeleted += async (sender, studentId) =>
            {
                await kafkaProducer.SendMessageAsync(new
                {
                    Status = "Success",
                    Message = $"Deleted student: {studentId}",
                    Timestamp = DateTime.UtcNow
                });
            };

            FieldAsync<StudentType>(
                "createStudent",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "age" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "class" }
                ),
                resolve: async context =>
                {
                    var student = new CreateStudentDto
                    {
                        Name = context.GetArgument<string>("name"),
                        Age = context.GetArgument<int>("age"),
                        Class = context.GetArgument<string>("class")
                    };
                    var studentCreated = await studentService.CreateStudentAsync(student);

                    //public event create
                    _studentEvent.OnStudentCreated(studentCreated);

                    return studentCreated;
                }
            );


            FieldAsync<StudentType>(
                "updateStudent",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "age" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "class" }
                ),
                resolve: async context =>
                {
                    var updatedStudent = await studentService.UpdateStudentAsync(
                      context.GetArgument<int>("id"),
                      new UpdateStudentDto
                      {
                        Name = context.GetArgument<string>("name"),
                        Age = context.GetArgument<int>("age"),
                        Class = context.GetArgument<string>("class")
                       }
                    );

                    _studentEvent.OnStudentUpdated(updatedStudent);
                    return updatedStudent;
                }
            );

            FieldAsync<BooleanGraphType>(
             "deleteStudent",
             arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
             ),
             resolve: async context =>
             {
                await studentService.DeleteStudentAsync(context.GetArgument<int>("id"));
                 _studentEvent.OnStudentDeleted(context.GetArgument<int>("id"));
                 return true;
             }
            );
        }
    }
}
