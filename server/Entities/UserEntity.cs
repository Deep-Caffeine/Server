using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Nickname { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$")]
        [Phone]
        public string Phone { get; set; }

        [Required]

        [RegularExpression(@"^\d{6}$")]
        public string Birth { get; set; }

        public string? ProfileURL { get; set; }

        public long Level { get; set; }

        public string Sns { get; set; }

        public UserEntity()
        {
            Email = "";
            Password = "";
            Username = "";
            Nickname = "";
            Phone = "";
            Birth = "";
            ProfileURL = null;
            Level = 0;
            Sns = "";
        }
    }
}
