using System.ComponentModel.DataAnnotations;

namespace GraphQLPractive.DTO
{
    public class CreateStudentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(16, 100)]
        public int Age { get; set; }

        [StringLength(50)]
        public string Class { get; set; }
    }
}
