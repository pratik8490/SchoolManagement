using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Core.Models
{
    public class BusStudentPickUpDropModel
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan PickUpTime { get; set; }
        public TimeSpan DropTime { get; set; }
    }
}