using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public int UserType { get; set; }
    }
}
