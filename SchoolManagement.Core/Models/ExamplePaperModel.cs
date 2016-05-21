using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Core.Models
{
    public class ExamplePaperModel
    {
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int IsPublished { get; set; }

        public List<ExamplePaperDetailModel> ExamplePaperDetails { get; set; }

        public ExamplePaperModel()
        {
            this.ExamplePaperDetails = new List<ExamplePaperDetailModel>();
        }
    }

    public class ExamplePaperDetailModel
    {
        public int Id { get; set; }
        public int ExamplePaperId { get; set; }
        public string FilePath { get; set; }
    }
}