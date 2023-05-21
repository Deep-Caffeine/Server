using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required(ErrorMessage = "이메일을 입력하지 않았습니다.")]
        [EmailAddress(ErrorMessage = "이메일 형식이 올바르지 않습니다.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "비밀번호를 입력하지 않았습니다.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[~!@#$%^&*()-=_+,.!?])(?!.*\s).{8,}$", ErrorMessage = "비밀번호는 8자 이상이어야 하며, 특수문자, 숫자, 알파벳이 반드시 포함되어야 합니다.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "사용자의 이름을 입력하지 않았습니다.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "사용자 이름의 최소 길이는 3자리 입니다.")]
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
