using GraphQL.Types;

namespace GraphQLPractive.Graphql
{
    public class StudentSchema : Schema
    {
        public StudentSchema(IServiceProvider services) : base(services)
        {
            Query = services.GetRequiredService<StudentQuery>();
            Mutation = services.GetRequiredService<StudentMutation>();
        }
    }
}
