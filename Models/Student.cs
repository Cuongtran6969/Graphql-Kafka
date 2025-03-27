using System.ComponentModel.DataAnnotations;

namespace GraphQLPractive.Model
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(16, 100)]
        public int Age { get; set; }

        [StringLength(50)]
        public string Class { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
