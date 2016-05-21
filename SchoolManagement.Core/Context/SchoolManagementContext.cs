using SchoolManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Context
{
    public class SchoolManagementContext
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static UserToken TokenResponseModel { get; set; }

        public static void Clear()
        {
            SchoolManagementContext.UserId = 0;
            SchoolManagementContext.UserName = string.Empty;
            SchoolManagementContext.IsLoggedIn = false;
            TokenResponseModel = new UserToken();
        }
    }
}
