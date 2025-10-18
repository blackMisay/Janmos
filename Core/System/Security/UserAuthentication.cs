using System;
using System.Data;
using System.Collections.Generic;
using Core.System.Data.Model;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace Core.System.Security
{
    public class UserAuthentication
    {

        public static bool IsAuthenticated(User account)
        {
            if (account == null)
                throw new SystemException("The provided account credential is invalid.");

            UserAuthentication ua = new UserAuthentication();
            if (ua.Authenticate(account))
            {
                if (ua.CheckStatus(account))
                {
                    return true;
                }
            }

            return false;
        }

        private bool Authenticate(User account)
        {
            string commandText = "SELECT * FROM User WHERE Username=@Username;";

            UpgradeManager db = new UpgradeManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "@Username", account.Username }
            };

            User user = new User();
            foreach (DataRow row in db.Load(commandText, parameters).Rows)
            {
                user.Username = row["username"].ToString();
                user.Password = row["password"].ToString();
                user.Key = row["key"].ToString();
            }
            if (user.Password != null)
            {
                if (user.Password.Equals(SecureHash.HashPassword(account.Password, user.Key)))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckStatus(User account)
        {
            string query = "SELECT u.status FROM user u WHERE username=@Username;";
            UpgradeManager um = new UpgradeManager();
            string strStatus = (um.ExecuteScalar(query, new Dictionary<string, string>
            {
                { "@Username", account.Username}
            })).ToString();
            if (string.IsNullOrWhiteSpace(strStatus))
            {
                return false;
            }
            else if (strStatus == "Deleted" || strStatus == "Inactive")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GenerateSecurityCode()
        {
            Random randomCode = new Random();
            return Convert.ToInt32(randomCode.Next(0, 1000000).ToString("D6"));
        }
        
        public bool ValidateUsername(string account)
        {
            // TODO: send securityCode for reset password
            string query = "SELECT * FROM user WHERE username = @Username OR email = @Email;";
            Dictionary<string, string> accountParams = new Dictionary<string, string>()
            {
                {"@Username", account },
                {"@Email", account }
            };
            UpgradeManager manager = new UpgradeManager();
            return Convert.ToBoolean(manager.ExecuteScalar(query, accountParams));
        }

        public string FetchAccount(string query, Dictionary<string, string> parameters)
        {
            UpgradeManager manager = new UpgradeManager();
            //return manager.GetStringExecuteScalar(query, parameters);
            return (manager.ExecuteScalar(query, parameters)).ToString();
        }

        public bool ResetPassword(User account)
        {
            string salt = SecureHash.GenerateSalt();
            string hashPassword = SecureHash.HashPassword(account.Password, salt);

            account.Key = salt;
            account.Password = hashPassword;

            UpgradeManager manager = new UpgradeManager();
            return manager.ExecuteQuery("UPDATE `user` SET `password`=@password,`key`=@key WHERE username=@username;",
                                 new Dictionary<string, string> { 
                                     { "@username", account.Username },
                                     { "@password", account.Password },
                                     { "@key", account.Key }
                                 });
        }
        public string GenerateSalt()
        {
            return SecureHash.GenerateSalt();
        }
        public string HashPassword(string password, string salt)
        {
            return SecureHash.HashPassword(password, salt);
        }
        /*public bool CreateUserAccountHardCoded(User account)
        {
            string salt = SecureHash.GenerateSalt();
            string hashpw = SecureHash.HashPassword(account.Password, salt);
            account.Key = salt;
            account.Password = hashpw;
            UpgradeManager um = new UpgradeManager();
            return um.ExecuteQuery("INSERT INTO `user`(username,`password`,`key`,createddate,`status`) VALUES(@Username,@`Password`,@`Key`,@Createddate,@Status);", new Dictionary<string, string>
            {
                { "@Username", account.Username},
                { "@`Password`", account.Password },
                { "@`Key`", account.Key },
                { "@Createddate", account.CreatedDate },
                { "@Status", account.Status.ToString() },
            });
        }*/

        public User FetchUserData(string username)
        {
            DataTable dt = new DataTable();
            UpgradeManager manager = new UpgradeManager();
            dt = manager.Load("SELECT * FROM dbjanmos.`user` WHERE `user`.username = '" + username + "';");

            if (dt.Rows.Count > 0)
            {
                User user = new User();
                foreach (DataRow row in dt.Rows)
                {
                    user.Id = Convert.ToInt32(row["id"]);
                    user.RolesId = new Roles() { Id = Convert.ToInt32(row["roleid"])};
                    user.UserInfoId = new UserInfo() { Id = Convert.ToInt32(row["userinfoid"]) };
                    user.Username = row["username"].ToString();
                }
                return user;
            }
            return null;
        }
    }
}