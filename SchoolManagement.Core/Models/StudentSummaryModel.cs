using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Models
{
    public class StudentSummaryModel
    {
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public string ClassTypeName { get; set; }
        public int Total { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public List<StudentCategory> StudentSummary { get; set; }
    }

    public class StudentCategory
    {
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public int Total { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }

    }
}
