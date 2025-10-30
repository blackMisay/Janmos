using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public static class ActionType
    {
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
        public const string RESTORE = "RESTORE";

        public const string LOGIN = "LOGIN";
        public const string LOGOUT = "LOGOUT";
        public const string VIEW = "VIEW";
        public const string PRINT = "PRINT";
        public const string DOWNLOAD = "DOWNLOAD";
        public const string PASSWORD_CHANGE = "PASSWORD CHANGE";
    }
}
