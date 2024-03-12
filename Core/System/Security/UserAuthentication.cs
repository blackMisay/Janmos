using System;
using System.Data;
using System.Collections.Generic;
using Core.System.Data.Model;

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
                return true;

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
    }
}