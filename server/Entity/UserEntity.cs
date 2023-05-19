using System;
namespace server.Entites
{
    public class UserEntity : BaseEntity
    {
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