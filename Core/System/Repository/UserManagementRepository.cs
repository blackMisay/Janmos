using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class UserManagementRepository
    {
        UpgradeManager manager;
        public DataTable LoadUserManagementData()
        {
            string query = "SELECT u.id AS 'ID', CONCAT(ui.givenname, ' ', ui.lastname) AS 'Full Name', r.roletitle AS 'Role', ui.email AS 'Email', u.lastlogin AS 'Last Login', CONCAT(creatorui.givenname, ' ', creatorui.lastname) AS 'Created By', u.createddate AS 'Date Created', u.`status` AS 'Status' FROM `user` u JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id LEFT JOIN `user` creator ON u.createdby = creator.id LEFT JOIN userinfo creatorui ON creator.userinfoid = creatorui.id WHERE u.`status` = 'Active' ORDER BY u.id DESC;";
            manager = new UpgradeManager();
            DataView dv = new DataView(manager.Load(query));
            dv.RowFilter = "Role <> 'Super Admin'";
            return dv.ToTable();
        }

        public DataTable LoadUserManagementData(string searchValue)
        {
            string query = "SELECT u.id AS 'ID', CONCAT(ui.givenname, ' ', ui.lastname) AS 'Full Name', r.roletitle AS 'Role', ui.email AS 'Email', u.lastlogin AS 'Last Login', CONCAT(creatorui.givenname, ' ', creatorui.lastname) AS 'Created By', u.createddate AS 'Date Created', u.`status` AS 'Status' FROM `user` u JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id LEFT JOIN `user` creator ON u.createdby = creator.id LEFT JOIN userinfo creatorui ON creator.userinfoid = creatorui.id WHERE u.`status` = 'Active' AND( u.id LIKE @val OR CONCAT(ui.givenname, ' ', ui.lastname) LIKE @val OR r.roletitle LIKE @val OR ui.email LIKE @val OR CONCAT(creatorui.givenname, ' ', creatorui.lastname) LIKE @val) ORDER BY u.id DESC;";
            manager = new UpgradeManager();

            Dictionary<string, string> userManagementParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };
            DataView dv = new DataView(manager.Load(query, userManagementParams));
            dv.RowFilter = "Role <> 'Super Admin'";
            return dv.ToTable();
        }
        public User FetchUser(int userId)
        {
            DataTable dt = new DataTable();
            manager = new UpgradeManager();
            dt = manager.Load("SELECT * FROM dbjanmos.user WHERE `user.id`=" + userId);

            if (dt.Rows.Count > 0)
            {
                User account = new User();
                foreach (DataRow row in dt.Rows)
                {
                    account.RolesId = new Roles() { Id = Convert.ToInt32(row["roleid"]) };
                    account.UserInfoId = new UserInfo() { Id = Convert.ToInt32(row["userinfoid"]) };
                    account.Username = row["username"].ToString();
                    account.Password = row["password"].ToString();
                    account.Key = row["key"].ToString();
                    account.Lastlogin = row["lastlogin"].ToString();
                    account.CreatedBy = new User() { Id = Convert.ToInt32(row["createdby"])};
                    account.CreatedDate = row["createddate"].ToString();
                }
                return account;
            }
            return null;
        }
        public UserInfo FetchUserInfo(int userinfoId)
        {
            DataTable dt = new DataTable();
            manager = new UpgradeManager();
            dt = manager.Load("SELECT * FROM dbjanmos.userinfo WHERE userinfo.id=" + userinfoId);

            if (dt.Rows.Count > 0)
            {
                UserInfo ui = new UserInfo();
                foreach (DataRow row in dt.Rows)
                {
                    ui.GivenName = row["givenname"].ToString();
                    ui.LastName = row["lastname"].ToString();
                    ui.Gender = (Gender)Enum.Parse(typeof(Gender), row["gender"].ToString());
                    ui.Contact = row["contact"].ToString();
                    ui.Address = row["address"].ToString();
                    ui.Email = row["email"].ToString();
                }
                return ui;
            }
            return null;
        }
        public User FetchUserData(int userId)
        {
            DataTable dt = new DataTable();
            manager = new UpgradeManager();
            dt = manager.Load("SELECT * FROM dbjanmos.user WHERE user.id=" + userId);

            if (dt.Rows.Count > 0)
            {
                User u = new User();
                foreach (DataRow row in dt.Rows)
                {
                    u.RolesId = new Roles { Id = Convert.ToInt32(row["roleid"]) };
                    u.UserInfoId = new UserInfo { Id = Convert.ToInt32(row["userinfoid"]) };
                    u.Username = row["username"].ToString();
                    u.Password = row["password"].ToString();
                    u.Key = row["key"].ToString();
                }
                return u;
            }
            return null;
        }
        public bool DeleteUserManagementData(int userManagementId)
        {
            string query = "UPDATE user u SET u.`status` = 'Deleted' WHERE u.id = @id";
            manager = new UpgradeManager();

            Dictionary<string, string> userManagementParams = new Dictionary<string, string>()
            {
                { "@id", userManagementId.ToString() }
            };
            return manager.ExecuteQuery(query, userManagementParams);
        }
        public object ExecuteScalar(string query, Dictionary<string, string> parameters)
        {
            manager = new UpgradeManager();
            return manager.ExecuteScalar(query, parameters);
        }
        public bool UpdateUserLastLogin(string Username, string Datetime)
        {
            string q = "UPDATE `user` u SET u.lastlogin = @Datetime WHERE u.username = @Username;";
            manager = new UpgradeManager();
            return manager.ExecuteQuery(q, new Dictionary<string, string> { { "@Username", Username }, { "@Datetime", Datetime } });
        }

        public int SaveInfo(UserInfo UInfo)
        {
            string query;
            if (UInfo.Id > 0)
            {
                query = "UPDATE userinfo ui SET ui.givenname = @Givenname, ui.lastname = @Lastname, ui.gender = @Gender, ui.contact = @Contact, ui.address = @Address, ui.email = @Email, ui.`status` = @`Status` WHERE ui.id = @Id;";
            }
            else
            {
                query = "INSERT INTO userinfo(givenname,lastname,gender,contact,address,email,`status`) VALUES(@Givenname,@Lastname,@Gender,@Contact,@Address,@Email,@`Status`); SELECT LAST_INSERT_ID();";
            }
            manager = new UpgradeManager();
            object result = manager.ExecuteScalar(query, new Dictionary<string, string>
            {
                { "@Id", UInfo.Id.ToString()},
                { "@Givenname", UInfo.GivenName},
                { "@Lastname", UInfo.LastName},
                { "@Gender", UInfo.Gender.ToString()},
                { "@Contact", UInfo.Contact},
                { "@Address", UInfo.Address},
                { "@Email", UInfo.Email},
                { "@Status", UInfo.Status.ToString()},
            });
            return result != null ? Convert.ToInt32(result) : 0;
        }
        public bool SaveUser(User u)
        {
            string query;
            if (u.Id > 0)
            {
                query = "UPDATE `user` u SET u.roleid = @RoleId, u.userinfoid = @UserinfoId, u.username = @Username, u.`password` = @`Password`, u.`key` = @`Key`, u.lastlogin = @Lastlogin, u.createdby = @Createdby, u.createddate = @Createddate, u.`status` = @`Status` WHERE u.id = @Id;";
            }
            else
            {
                query = "INSERT INTO `user`(roleid,userinfoid,username,`password`,`key`,lastlogin,createdby,createddate,`status`) VALUES(@RoleId,@UserinfoId,@Username,@`Password`,@`Key`,@Lastlogin,@Createdby,@Createddate,@`Status`);";
            }
            manager = new UpgradeManager();
            if (manager.ExecuteQuery(query, new Dictionary<string, string>
            {
                { "@Id", u.Id.ToString()},
                { "@RoleId", u.RolesId.Id.ToString()},
                { "@UserinfoId", u.UserInfoId.Id.ToString()},
                { "@Username", u.Username},
                { "@`Password`", u.Password},
                { "@`Key`", u.Key},
                { "@Lastlogin", u.Lastlogin},
                { "@Createdby", u.CreatedBy.Id.ToString()},
                { "@Createddate", u.CreatedDate},
                { "@`Status`", u.Status.ToString()}
            })) return true;
            return false;
        }
        public DataTable LoadDataList(string query)
        {
            this.manager = new UpgradeManager();
            return this.manager.Load(query);
        }
    }
}
