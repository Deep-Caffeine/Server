using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class SchoolInformationEntity : BaseEntity
    {
        [Required]
        public long User { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string State { get; set; }

        public int? Grade { get; set; }
        
    }
}
