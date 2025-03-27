using GraphQL.Types;
using GraphQLPractive.DTO;
using GraphQLPractive.Model;

namespace GraphQLPractive.Graphql
{
    public class StudentType : ObjectGraphType<StudentDto>
    {
        public StudentType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("ID sinh viên");
            Field(x => x.Name).Description("Tên sinh viên");
            Field(x => x.Age).Description("Tuổi sinh viên");
            Field(x => x.Class).Description("Lớp");
            Field(x => x.CreatedAt).Description("Ngày tạo");
            Field(x => x.UpdatedAt).Description("Ngày caapj nhaatj");
        }
    }
}
