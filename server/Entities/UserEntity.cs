using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required(ErrorMessage = "이메일을 입력하지 않았습니다.")]
        [EmailAddress(ErrorMessage = "이메일 형식이 올바르지 않습니다.")]
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Username { get; set; }
        
        public string Phone { get; set; }
        
        public string Birth { get; set; }
        
        public string ProfileURL { get; set; }

        public long Level { get; set; }

        public string Sns { get; set; }

        public UserEntity()
        {
            Email = "";
            Password = "";
            Username = "";
            Phone = "";
            Birth = "";
            ProfileURL = "";
            Level = 0;
            Sns = "";
        }
    }
}
