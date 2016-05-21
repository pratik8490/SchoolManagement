using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Models
{
    public class DigitalBookLibraryModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookPath { get; set; }
        public int StadardId { get; set; }
        public string StandardName { get; set; }
        public int DigitalBookTypeId { get; set; }
        public string DigitalBookTypeName { get; set; }
    }

    public class DigitalBookTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
