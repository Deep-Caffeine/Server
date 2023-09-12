using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace server.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }
        
        [Required]
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        public DateTime Birth { get; set; }
        
        public string? ProfileURL { get; set; }

        public long Level { get; set; }

        public string Sns { get; set; }

        public UserEntity()
        {
            Email = "";
            Password = "";
            Username = "";
            Phone = "";
            Birth = new DateTime();
            ProfileURL = null;
            Level = 0;
            Sns = "";
        }
    }
}
