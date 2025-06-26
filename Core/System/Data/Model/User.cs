using System;

namespace Core.System.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        public Roles RolesId { get; set; }
        public UserInfo UserInfoId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Key { get; set; }
        public String Lastlogin { get; set; }
        public User CreatedBy { get; set; }
        public String CreatedDate { get; set; }
        public StatusRecord.Type Status { get; set; }
    }
}
