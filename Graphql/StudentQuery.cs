using GraphQL;
using GraphQL.Types;
using GraphQLPractive.Services;

namespace GraphQLPractive.Graphql
{
    public class StudentQuery : ObjectGraphType
    {
        public StudentQuery(IStudentService studentService)
        {
            FieldAsync<ListGraphType<StudentType>>(
            "students",
            resolve: async context => await studentService.GetAllStudentsAsync()
            );

            FieldAsync<StudentType>(
             "student",
             arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
             resolve: async context => await studentService.GetStudentByIdAsync(context.GetArgument<int>("id"))
             );
        }
    }
}
