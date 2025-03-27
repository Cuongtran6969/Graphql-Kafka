using System.ComponentModel.DataAnnotations;

namespace GraphQLPractive.DTO
{
    public class UpdateStudentDto
    {
        [StringLength(100)]
        public string Name { get; set; }
        [Range(16, 100)]
        public int Age { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
    }
}
