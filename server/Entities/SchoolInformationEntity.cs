using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace server.Entities
{
    public class SchoolInformationEntity : BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string State { get; set; }

        public int? Grade { get; set; }

    }
}
